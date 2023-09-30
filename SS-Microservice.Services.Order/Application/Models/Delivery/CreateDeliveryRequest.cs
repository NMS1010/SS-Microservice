namespace SS_Microservice.Services.Order.Application.Models.Delivery
{
    public class CreateDeliveryRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public IFormFile Image { get; set; }
    }
}