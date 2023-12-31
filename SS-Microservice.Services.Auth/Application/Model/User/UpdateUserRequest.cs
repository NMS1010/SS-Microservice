﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SS_Microservice.Services.Auth.Application.Model.User
{
    public class UpdateUserRequest
    {
        [JsonIgnore]
        public string UserId { get; set; }

        public string Phone { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Dob { get; set; }
        public string Gender { get; set; }
        public IFormFile Avatar { get; set; }
        public int Status { get; set; }
    }
}