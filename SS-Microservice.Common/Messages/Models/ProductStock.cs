using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SS_Microservice.Common.Messages.Models
{
    public class ProductStock
    {
        public string ProductId { get; set; }
        public long Quantity { get; set; }
    }
}