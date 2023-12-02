namespace SS_Microservice.Services.UserOperation.Infrastructure.Services.Order.Model.Response
{
    public class OrderItemDto
    {
        public long Id { get; set; }
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
