using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SS_Microservice.Common.Messages.Events.Basket
{
    public class BasketClearedEvent
    {
        public string OrderId { get; set; }
    }
}