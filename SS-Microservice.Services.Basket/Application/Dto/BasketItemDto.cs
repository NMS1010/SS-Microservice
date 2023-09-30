using SS_Microservice.Common.Entities;

namespace SS_Microservice.Services.Basket.Application.Dto
{
    public class BasketItemDto : BaseAuditableEntity<long>
    {
        public string VariantId { get; set; }
        public string VariantName { get; set; }
        public long VariantQuantity { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductUnit { get; set; }
        public decimal Price { get; set; }
        public decimal PromotionalPrice { get; set; }
        public string DefaultImage { get; set; }
        public string CategoryName { get; set; }
        public string BrandName { get; set; }
        public string CategorySlug { get; set; }
        public string Status { get; set; }
        public string Slug { get; set; }
        public long Quantity { get; set; }
        public long Rating { get; set; }
        public long Sold { get; set; }
        public int IsSelected { get; set; } = 0;
    }
}