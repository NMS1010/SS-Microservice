using SS_Microservice.Contracts.Events.Order;

namespace SS_Microservice.SagaOrchestration.Messaging.Events.Order
{
    public class OrderCreationCompletedEvent : IOrderCreationCompletedEvent
    {

        public Guid CorrelationId { get; set; }
        public string OrderCode { get; set; }
        public string UserId { get; set; }
    }
}
