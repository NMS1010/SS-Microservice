﻿using Consul;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SS_Microservice.Common.Consul
{
    public class ServiceDiscoveryHostedService : IHostedService
    {
        private readonly IConsulClient _client;
        private readonly ConsulConfig _config;
        private AgentServiceRegistration _registration;

        public ServiceDiscoveryHostedService(IConsulClient client, ConsulConfig config)
        {
            _client = client;
            _config = config;
        }

        // Registers service to Consul registry
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _registration = new AgentServiceRegistration
            {
                ID = _config.Id,
                Name = _config.Name,
                Address = _config.Address,
                Port = _config.Port,
                Check = new AgentServiceCheck()
                {
                    Interval = TimeSpan.FromSeconds(15),
                    HTTP = $"https://{_config.Address}:{_config.Port}/{_config.HealthCheckEndPoint}",
                    Timeout = TimeSpan.FromSeconds(5)
                }
            };

            // Deregister already registered service
            await _client.Agent.ServiceDeregister(_registration.ID, cancellationToken).ConfigureAwait(false);

            // Registers service
            await _client.Agent.ServiceRegister(_registration, cancellationToken).ConfigureAwait(false);
        }

        // If the service is shutting down it deregisters service from Consul registry
        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _client.Agent.ServiceDeregister(_registration.ID, cancellationToken).ConfigureAwait(false);
        }
    }
}