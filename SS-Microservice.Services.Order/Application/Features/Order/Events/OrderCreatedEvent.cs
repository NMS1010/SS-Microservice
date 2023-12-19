using SS_Microservice.Contracts.Events.Order;
using SS_Microservice.Contracts.Models;

namespace SS_Microservice.Services.Order.Application.Features.Order.Events
{
    public class OrderCreatedEvent : IOrderCreatedEvent
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public long OrderId { get; set; }
        public string OrderCode { get; set; }
        public decimal TotalPrice { get; set; }
        public string PaymentMethod { get; set; }
        public List<ProductStock> Products { get; set; }

        public Guid CorrelationId { get; set; }

        public string Address { get; set; }
        public string Receiver { get; set; }
        public string ReceiverEmail { get; set; }
        public string Phone { get; set; }
    }
}
