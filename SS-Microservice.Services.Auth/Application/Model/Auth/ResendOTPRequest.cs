using System.Text.Json.Serialization;

namespace SS_Microservice.Services.Auth.Application.Model.Auth
{
    public class ResendOTPRequest
    {
        public string Email { get; set; }

        [JsonIgnore]
        public string Type { get; set; }
    }
}