using SS_Microservice.Contracts.Events.Order;
using SS_Microservice.Contracts.Models;

namespace SS_Microservice.SagaOrchestration.Messaging.Events.Order
{
    public class OrderCreatedEvent : IOrderCreatedEvent
    {
        public string UserId { get; set; }
        public long OrderId { get; set; }
        public string OrderCode { get; set; }
        public decimal TotalPrice { get; set; }
        public string PaymentMethod { get; set; }
        public List<ProductStock> Products { get; set; }

        public Guid CorrelationId { get; set; }
    }
}
