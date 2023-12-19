using System.Text.Json.Serialization;

namespace SS_Microservice.Services.Order.Application.Models.Order
{
    public class CreateOrderItemRequest
    {
        public int Quantity { get; set; }
        public long VariantId { get; set; }

        [JsonIgnore]
        public decimal TotalPrice { get; set; }
    }
}