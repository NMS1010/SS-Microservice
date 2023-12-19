using SS_Microservice.Contracts.Events.Product;

namespace SS_Microservice.SagaOrchestration.Messaging.Events.Product
{
    public class StockReservedEvent : IStockReservedEvent
    {
        public Guid CorrelationId { get; set; }
        public string Image { get; set; }
    }
}
