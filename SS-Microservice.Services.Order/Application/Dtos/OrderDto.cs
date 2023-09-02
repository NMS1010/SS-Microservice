namespace SS_Microservice.Services.Order.Application.Dtos
{
    public class OrderDto
    {
        public string OrderId { get; set; }

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
        public string OrderStateName { get; set; }

        public List<OrderItemDto> OrderItems { get; set; }
    }
}