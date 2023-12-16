using SS_Microservice.Contracts.Events.Inventory;

namespace SS_Microservice.SagaOrchestration.Messaging.Events.Inventory
{
    public class InventoryExportationRejectedEvent : IInventoryExportationRejectedEvent
    {
        public Guid CorrelationId { get; set; }
    }
}
