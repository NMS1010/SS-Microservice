using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SS_Microservice.Common.Consul
{
    public class ConsulConfig
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public int Port { get; set; }

        public Uri DiscoveryAddress { get; set; }

        public string HealthCheckEndPoint { get; set; }
    }
}