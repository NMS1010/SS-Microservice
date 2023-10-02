using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SS_Microservice.Common.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SS_Microservice.Common.RabbitMQ
{
    public static class Extension
    {
        [Obsolete]
        public static IServiceCollection AddMessaging(this IServiceCollection services, IConfiguration configuration, List<Type> consumers = null)
        {
            var rabbitMqSettings = configuration.GetOptions<RabbitMqSettings>("RabbitMqSettings");
            services
                .AddMassTransit(mt =>
                {
                    consumers?.ForEach(consumer => mt.AddConsumer(consumer));

                    mt.AddBus(bus => Bus.Factory.CreateUsingRabbitMq(rmq =>
                    {
                        rmq.Host(rabbitMqSettings.Uri);

                        consumers?.ForEach(consumer => rmq.ReceiveEndpoint(consumer.FullName, endpoint =>
                        {
                            endpoint.ConfigureConsumer(bus, consumer);
                        }));
                    }));
                })
                .AddMassTransitHostedService();

            return services;
        }
    }
}