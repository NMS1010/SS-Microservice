using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SS_Microservice.Common.Messages.Events.Product
{
    public class ProductReservedEvent
    {
        public string OrderId { get; set; }
        public string UserId { get; set; }
    }
}