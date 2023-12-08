using System.Text.Json.Serialization;

namespace SS_Microservice.Services.Order.Application.Models.Order
{
    public class CompletePaypalOrderRequest
    {
        [JsonIgnore]
        public string UserId { get; set; }

        [JsonIgnore]
        public long OrderId { get; set; }

        public string PaypalOrderId { get; set; } = null;
        public string PaypalOrderStatus { get; set; } = null;
    }
}