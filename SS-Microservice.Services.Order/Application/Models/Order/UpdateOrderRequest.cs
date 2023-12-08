using System.Text.Json.Serialization;

namespace SS_Microservice.Services.Order.Application.Models.Order
{
    public class UpdateOrderRequest
    {
        [JsonIgnore]
        public string UserId { get; set; }

        [JsonIgnore]
        public long OrderId { get; set; }

        public string Status { get; set; }
        public string OtherCancellation { get; set; }
        public long? OrderCancellationReasonId { get; set; }
    }
}