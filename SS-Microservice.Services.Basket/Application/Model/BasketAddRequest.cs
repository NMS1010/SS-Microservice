namespace SS_Microservice.Services.Basket.Application.Model
{
    public class BasketAddRequest
    {
        public string UserId { get; set; }
        public string VariantId { get; set; }
        public long Quantity { get; set; }
    }
}