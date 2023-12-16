using SS_Microservice.Contracts.Events.Inventory;

namespace SS_Microservice.SagaOrchestration.Messaging.Events.Inventory
{
    public class InventoryExportedEvent : IInventoryExportedEvent
    {
        public Guid CorrelationId { get; set; }
    }
}
