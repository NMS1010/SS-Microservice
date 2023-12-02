using System.Text.Json.Serialization;

namespace SS_Microservice.Services.UserOperation.Application.Models.UserFollowProduct
{
    public class UnFollowProductRequest
    {
        [JsonIgnore]
        public string UserId { get; set; }
        public long ProductId { get; set; }
    }
}
