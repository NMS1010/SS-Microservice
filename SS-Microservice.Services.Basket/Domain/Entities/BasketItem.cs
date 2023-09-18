using SS_Microservice.Common.Entities;

namespace SS_Microservice.Services.Basket.Domain.Entities
{
    public class BasketItem : BaseAuditableEntity<int>
    {
        public int BasketId { get; set; }
        public string ProductId { get; set; }
        public long Quantity { get; set; }
        public int IsSelected { get; set; } = 0;

        public Basket Basket { get; set; }
    }
}