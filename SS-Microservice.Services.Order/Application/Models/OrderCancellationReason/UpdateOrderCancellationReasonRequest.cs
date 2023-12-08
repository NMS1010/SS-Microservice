using System.Text.Json.Serialization;

namespace SS_Microservice.Services.Order.Application.Models.OrderCancellationReason
{
    public class UpdateOrderCancellationReasonRequest
    {
        [JsonIgnore]
        public long Id { get; set; }

        public string Name { get; set; }
        public string Note { get; set; }
        public bool Status { get; set; }
    }
}