using SS_Microservice.Common.Entities;

namespace SS_Microservice.Services.Products.Domain.Entities
{
    public class Variant : BaseAuditableEntity<string>
    {
        public string Name { get; set; }
        public string Sku { get; set; }
        public decimal ItemPrice { get; set; }
        public decimal PromotionalItemPrice { get; set; }
        public string Status { get; set; }
        public long Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    }
}