namespace SS_Microservice.Services.Basket.Core.Entities
{
    public class BasketItem
    {
        public int BasketItemId { get; set; }
        public long BasketId { get; set; }
        public string ProductId { get; set; }
        public long Quantity { get; set; }
        public int IsSelected { get; set; } = 0;
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }

        public Basket Basket { get; set; }
    }
}