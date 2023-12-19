using SS_Microservice.Contracts.Commands.Inventory;

namespace SS_Microservice.SagaOrchestration.Messaging.Commands.Inventory
{
    public class RollBackInventoryCommand : IRollBackInventoryCommand
    {
        public Guid CorrelationId { get; set; }
        public long OrderId { get; set; }
    }
}
