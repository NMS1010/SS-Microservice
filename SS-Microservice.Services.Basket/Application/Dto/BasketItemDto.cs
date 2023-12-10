using SS_Microservice.Common.Types.Entities;

namespace SS_Microservice.Services.Basket.Application.Dto
{
    public class BasketItemDto : BaseAuditableEntity<long>
    {
        public int Quantity { get; set; }
        public long VariantId { get; set; }
        public string VariantName { get; set; }
        public long VariantQuantity { get; set; }
        public string Sku { get; set; }
        public decimal VariantPrice { get; set; }
        public decimal? VariantPromotionalPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal? TotalPromotionalPrice { get; set; }
        public string ProductSlug { get; set; }
        public string ProductUnit { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public bool IsPromotion { get; set; }
    }
}