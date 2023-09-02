namespace SS_Microservice.Services.Order.Application.Dtos
{
    public class OrderItemDto
    {
        public int OrderItemId { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}