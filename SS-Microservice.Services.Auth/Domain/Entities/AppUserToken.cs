﻿using SS_Microservice.Common.Types.Entities;

namespace SS_Microservice.Services.Auth.Domain.Entities
{
    public class AppUserToken : BaseAuditableEntity<long>
    {
        public AppUser User { get; set; }
        public string UserId { get; set; }
        public string Token { get; set; }
        public DateTime? ExpiredAt { get; set; }
        public string Type { get; set; }
    }
}