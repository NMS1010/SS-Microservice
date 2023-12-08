using SS_Microservice.Common.Entities;

namespace SS_Microservice.Services.Order.Application.Dtos
{
    public class OrderItemDto : BaseAuditableEntity<long>
    {
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public string Sku { get; set; }
        public long ProductId { get; set; }
        public string ProductSlug { get; set; }
        public string ProductUnit { get; set; }
        public string ProductImage { get; set; }
        public string ProductName { get; set; }
        public string VariantName { get; set; }
        public long VariantQuantity { get; set; }
    }
}