﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SS_Microservice.Common.Messages.Events.Product
{
    public class ProductInventoryUpdatedRejectedEvent
    {
        public long OrderId { get; set; }
    }
}