using SS_Microservice.Contracts.Events.Basket;

namespace SS_Microservice.SagaOrchestration.Messaging.Events.Basket
{
    public class BasketClearedRejectedEvent : IBasketClearedRejectedEvent
    {
        public Guid CorrelationId { get; set; }
    }
}
