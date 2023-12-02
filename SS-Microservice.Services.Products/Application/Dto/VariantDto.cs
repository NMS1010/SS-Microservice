using SS_Microservice.Common.Entities;

namespace SS_Microservice.Services.Products.Application.Dto
{
    public class VariantDto : BaseAuditableEntity<long>
    {
        public string Name { get; set; }
        public long ProductId { get; set; }
        public string Sku { get; set; }
        public int Quantity { get; set; }
        public decimal ItemPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal? TotalPromotionalPrice { get; set; }
        public decimal? PromotionalItemPrice { get; set; }
        public string Status { get; set; }
    }
}