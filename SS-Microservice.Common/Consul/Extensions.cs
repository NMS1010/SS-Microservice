﻿using Consul;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SS_Microservice.Common.Configuration;

namespace SS_Microservice.Common.Consul
{
    public static class Extensions
    {
        public static ConsulConfig GetConsulConfig(this IConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            return configuration.GetOptions<ConsulConfig>("ConsulConfig");
        }

        public static void AddConsul(this IServiceCollection services, ConsulConfig consulConfig)
        {
            if (consulConfig == null)
            {
                throw new ArgumentNullException(nameof(consulConfig));
            }

            var consulClient = new ConsulClient(config =>
            {
                config.Address = consulConfig.DiscoveryAddress;
            });

            services.AddSingleton(consulConfig);
            services.AddSingleton<IConsulClient, ConsulClient>(_ => consulClient);
            services.AddSingleton<IHostedService, ServiceDiscoveryHostedService>();
        }
    }
}