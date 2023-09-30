using SS_Microservice.Common.Entities;

namespace SS_Microservice.Services.Basket.Domain.Entities
{
    public class BasketItem : BaseAuditableEntity<long>
    {
        public long BasketId { get; set; }
        public string VariantId { get; set; }
        public long Quantity { get; set; }
        public int IsSelected { get; set; } = 0;

        public Basket Basket { get; set; }
    }
}