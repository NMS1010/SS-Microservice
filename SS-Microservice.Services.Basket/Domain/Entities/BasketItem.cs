using SS_Microservice.Common.Types.Entities;

namespace SS_Microservice.Services.Basket.Domain.Entities
{
    public class BasketItem : BaseAuditableEntity<long>
    {
        public long BasketId { get; set; }
        public long VariantId { get; set; }
        public long Quantity { get; set; }

        public Basket Basket { get; set; }
    }
}