using System.Text.Json.Serialization;

namespace SS_Microservice.Services.UserOperation.Application.Models.UserFollowProduct
{
    public class FollowProductRequest
    {
        [JsonIgnore]
        public string UserId { get; set; }
        public long ProductId { get; set; }
    }
}
