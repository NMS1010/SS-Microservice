using System.Text.Json.Serialization;

namespace SS_Microservice.Services.Basket.Application.Model
{
    public class UpdateBasketItemRequest
    {
        [JsonIgnore]
        public string UserId { get; set; }

        [JsonIgnore]
        public long CartItemId { get; set; }

        public int Quantity { get; set; }
    }
}