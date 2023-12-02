using System.Text.Json.Serialization;

namespace SS_Microservice.Services.UserOperation.Application.Models.Review
{
    public class ReplyReviewRequest
    {
        [JsonIgnore]
        public long Id { get; set; }
        public string Reply { get; set; }
    }
}