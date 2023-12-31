﻿using SS_Microservice.Common.Types.Entities;

namespace SS_Microservice.Services.Address.Domain.Entities
{
    public class Address : BaseAuditableEntity<long>
    {
        public string UserId { get; set; }
        public string Receiver { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Street { get; set; }

        public bool IsDefault { get; set; } = true;
        public bool Status { get; set; } = true;

        public long ProvinceId { get; set; }
        public long DistrictId { get; set; }
        public long WardId { get; set; }
        public Province Province { get; set; }
        public District District { get; set; }
        public Ward Ward { get; set; }
    }
}