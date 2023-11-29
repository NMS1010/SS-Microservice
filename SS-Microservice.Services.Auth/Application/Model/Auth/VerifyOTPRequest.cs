using System.Text.Json.Serialization;

namespace SS_Microservice.Services.Auth.Application.Model.Auth
{
    public class VerifyOTPRequest
    {
        public string Email { get; set; }
        public string OTP { get; set; }

        [JsonIgnore]
        public string Type { get; set; }
    }
}