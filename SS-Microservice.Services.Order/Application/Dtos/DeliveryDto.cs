﻿using SS_Microservice.Common.Types.Entities;

namespace SS_Microservice.Services.Order.Application.Dtos
{
    public class DeliveryDto : BaseAuditableEntity<long>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public bool Status { get; set; }
    }
}