namespace SS_Microservice.Services.Order.Core.Entities
{
    public class Order
    {
        public string OrderId { get; set; }
        public string UserId { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public decimal TotalItemPrice { get; set; }
        public decimal TotalPrice { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }

        public DateTime DateDone { get; set; }
        public DateTime DatePaid { get; set; }

        public int OrderStateId { get; set; }
        public OrderState OrderState { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}