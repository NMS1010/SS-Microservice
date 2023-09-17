using SS_Microservice.Common.Messages.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SS_Microservice.Common.Messages.Events.Product
{
    public class ProductReleasedEvent
    {
        public string OrderId { get; set; }
    }
}