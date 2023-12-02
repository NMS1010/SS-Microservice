using System.Text.Json.Serialization;

namespace SS_Microservice.Services.UserOperation.Application.Models.Review
{
    public class CreateReviewRequest
    {
        [JsonIgnore]
        public string UserId { get; set; }
        public long ProductId { get; set; }
        public long OrderItemId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int Rating { get; set; }
        public IFormFile Image { get; set; }
    }
}
