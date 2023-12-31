﻿using SS_Microservice.Common.Types.Entities;
using SS_Microservice.Services.Auth.Infrastructure.Services.Address.Model.Response;

namespace SS_Microservice.Services.Auth.Application.Dto
{
    public class UserDto : BaseAuditableEntity<string>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime? Dob { get; set; }
        public string Gender { get; set; }
        public string Avatar { get; set; }
        public int Status { get; set; }

        public List<string> Roles { get; set; } = new List<string>();
        public List<AddressDto> Addresses { get; set; }
    }
}