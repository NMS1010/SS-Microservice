using System.Text.Json.Serialization;

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