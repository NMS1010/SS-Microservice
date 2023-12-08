using System.Text.Json.Serialization;

namespace SS_Microservice.Services.Order.Application.Models.Delivery
{
    public class UpdateDeliveryRequest
    {
        [JsonIgnore]
        public long Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public IFormFile Image { get; set; }
        public bool Status { get; set; }
    }
}