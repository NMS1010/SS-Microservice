namespace SS_Microservice.Services.Basket.Application.Dto
{
    public class BasketItemDto
    {
        public int BasketItemId { get; set; }
        public string ProductId { get; set; }
        public long Quantity { get; set; }
        public int IsSelected { get; set; } = 0;
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}