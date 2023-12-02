using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestEase;
using SS_Microservice.Common.Configuration;
using SS_Microservice.Common.Exceptions;

namespace SS_Microservice.Common.RestEase
{
    public static class Extension
    {
        public static IServiceCollection RegisterServiceForwarder<T>(this IServiceCollection services, string serviceName)
            where T : class
        {
            var clientName = typeof(T).ToString();
            var options = ConfigureOptions(services);
            switch (options.LoadBalancer?.ToLowerInvariant())
            {
                //case "consul":
                //    ConfigureConsulClient(services, clientName, serviceName);
                //    break;
                //case "fabio":
                //    ConfigureFabioClient(services, clientName, serviceName);
                //break;
                default:
                    ConfigureDefaultClient(services, clientName, serviceName, options);
                    break;
            }

            ConfigureForwarder<T>(services, clientName);

            return services;
        }

        private static RestEaseOptions ConfigureOptions(IServiceCollection services)
        {
            IConfiguration configuration;
            using (var serviceProvider = services.BuildServiceProvider())
            {
                configuration = serviceProvider.GetService<IConfiguration>();
            }

            services.Configure<RestEaseOptions>(configuration.GetSection("RestEase"));

            return configuration.GetOptions<RestEaseOptions>("RestEase");
        }

        private static void ConfigureDefaultClient(IServiceCollection services, string clientName, string serviceName,
                       RestEaseOptions options)
        {

            services.AddHttpClient(clientName, c =>
            {
                var service = (options.Services?.FirstOrDefault(x => x.Name.Equals(serviceName,
                    StringComparison.InvariantCultureIgnoreCase)))
                    ?? throw new RestEaseServiceNotFoundException($"RestEase service: '{serviceName}' was not found.",
                        serviceName);

                c.BaseAddress = new UriBuilder
                {
                    Scheme = service.Scheme,
                    Host = service.Host,
                    Port = service.Port
                }.Uri;

            });
        }

        private static void ConfigureForwarder<T>(IServiceCollection services, string clientName) where T : class
        {
            services.AddTransient<T>(c =>
                new RestClient(
                    c.GetService<IHttpClientFactory>()
                     .CreateClient(clientName)
                ).For<T>());
        }
    }
}
