using System.Text.Json.Serialization;

namespace SS_Microservice.Services.UserOperation.Application.Models.Review
{
    public class UpdateReviewRequest
    {
        [JsonIgnore]
        public long Id { get; set; }

        [JsonIgnore]
        public string UserId { get; set; }

        public string Title { get; set; }
        public string Content { get; set; }
        public int Rating { get; set; }
        public IFormFile Image { get; set; }
        public bool IsDeleteImage { get; set; }
    }
}