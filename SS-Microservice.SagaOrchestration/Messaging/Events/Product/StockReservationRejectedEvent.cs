using SS_Microservice.Contracts.Events.Product;

namespace SS_Microservice.SagaOrchestration.Messaging.Events.Product
{
    public class StockReservationRejectedEvent : IStockReservationRejectedEvent
    {
        public Guid CorrelationId { get; set; }
    }
}
