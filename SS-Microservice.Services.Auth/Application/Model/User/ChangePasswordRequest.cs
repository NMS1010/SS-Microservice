using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SS_Microservice.Services.Auth.Application.Model.User
{
    public class ChangePasswordRequest
    {
        [JsonIgnore]
        public string UserId { get; set; }

        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}