namespace SS_Microservice.Services.Basket.Application.Dto
{
    public class BasketItemDto
    {
        public int BasketItemId { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal Price { get; set; }
        public string MainImage { get; set; }
        public string Origin { get; set; }
        public long Quantity { get; set; }
        public int IsSelected { get; set; } = 0;
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}