using System.Text.Json.Serialization;

namespace SS_Microservice.Services.Basket.Application.Model
{
    public class CreateBasketItemRequest
    {
        [JsonIgnore]
        public string UserId { get; set; }

        public long VariantId { get; set; }
        public int Quantity { get; set; } = 1;
    }
}