﻿using SS_Microservice.Common.Entities;

namespace SS_Microservice.Services.Order.Application.Dtos
{
    public class OrderStateDto : BaseAuditableEntity<long>
    {
        public string OrderStateName { get; set; }
        public int Order { get; set; }
        public string HexColor { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}