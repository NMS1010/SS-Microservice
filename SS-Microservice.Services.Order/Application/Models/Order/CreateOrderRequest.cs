using System.Text.Json.Serialization;

namespace SS_Microservice.Services.Order.Application.Models.Order
{
    public class CreateOrderRequest
    {
        [JsonIgnore]
        public string UserId { get; set; }

        [JsonIgnore]
        public long AddressId { get; set; }

        public string Note { get; set; }
        public long PaymentMethodId { get; set; }
        public long DeliveryId { get; set; }

        public List<CreateOrderItemRequest> Items { get; set; }
    }
}