using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SS_Microservice.Common.Configuration;
using System.Reflection;

namespace SS_Microservice.Common.RabbitMQ
{
    public static class Extension
    {
        public static IServiceCollection AddMessaging(this IServiceCollection services, IConfiguration configuration)
        {
            var rabbitMqSettings = configuration.GetOptions<RabbitMqSettings>("RabbitMqSettings");
            services
                .AddMassTransit(mt =>
                {
                    mt.AddDelayedMessageScheduler();

                    mt.SetKebabCaseEndpointNameFormatter();

                    var entryAssembly = Assembly.GetEntryAssembly();
                    mt.AddConsumers(entryAssembly);
                    mt.AddActivities(entryAssembly);
                    mt.AddSagas(entryAssembly);
                    mt.AddSagaStateMachines(entryAssembly);

                    mt.UsingRabbitMq((context, cfg) =>
                    {
                        cfg.UseDelayedMessageScheduler();
                        cfg.Host(rabbitMqSettings.Uri, "/", h =>
                        {
                            h.Username(rabbitMqSettings.UserName);
                            h.Password(rabbitMqSettings.Password);
                        });

                        cfg.ConfigureEndpoints(context);
                    });
                });

            return services;
        }
    }
}