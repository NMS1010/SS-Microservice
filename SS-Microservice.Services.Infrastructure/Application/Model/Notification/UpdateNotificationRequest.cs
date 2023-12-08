using System.Text.Json.Serialization;

namespace SS_Microservice.Services.Infrastructure.Application.Model.Notification
{
    public class UpdateNotificationRequest
    {
        [JsonIgnore]
        public long Id { get; set; }

        [JsonIgnore]
        public string UserId { get; set; }
    }
}