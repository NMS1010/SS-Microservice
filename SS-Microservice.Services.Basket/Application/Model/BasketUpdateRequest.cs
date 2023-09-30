namespace SS_Microservice.Services.Basket.Application.Model
{
    public class BasketUpdateRequest
    {
        public string UserId { get; set; }
        public long BasketItemId { get; set; }
        public long Quantity { get; set; }
    }
}