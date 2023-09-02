namespace SS_Microservice.Services.Order.Core.Entities
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public string OrderId { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public Order Order { get; set; }
    }
}