using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SS_Microservice.Common.RabbitMQ
{
    public class EventBusConsumer
    {
        public Type Type { get; set; }
        public string Endpoint { get; set; }
    }
}