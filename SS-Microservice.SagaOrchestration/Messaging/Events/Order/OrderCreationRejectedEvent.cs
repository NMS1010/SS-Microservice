using SS_Microservice.Contracts.Events.Order;

namespace SS_Microservice.SagaOrchestration.Messaging.Events.Order
{
    public class OrderCreationRejectedEvent : IOrderCreationRejectedEvent
    {
        public Guid CorrelationId { get; set; }

        public long OrderId { get; set; }
        public string UserId { get; set; }
    }
}
