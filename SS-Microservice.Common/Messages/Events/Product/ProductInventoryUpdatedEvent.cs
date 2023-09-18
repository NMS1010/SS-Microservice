﻿using SS_Microservice.Common.Messages.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SS_Microservice.Common.Messages.Events.Product
{
    public class ProductInventoryUpdatedEvent
    {
        public long OrderId { get; set; }
        public string UserId { get; set; }
        public List<ProductStock> Products { get; set; }
    }
}