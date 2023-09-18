using SS_Microservice.Common.Entities;

namespace SS_Microservice.Services.Order.Domain.Entities
{
    public class Order : BaseAuditableEntity<long>
    {
        public string UserId { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public decimal TotalItemPrice { get; set; }
        public decimal TotalPrice { get; set; }

        public DateTime? DateDone { get; set; }
        public DateTime? DatePaid { get; set; }

        public long OrderStateId { get; set; }
        public OrderState OrderState { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}