using SS_Microservice.Contracts.Commands.Inventory;
using SS_Microservice.Contracts.Models;

namespace SS_Microservice.SagaOrchestration.Messaging.Commands.Inventory
{
    public class ExportInventoryCommand : IExportInventoryCommand
    {
        public long OrderId { get; set; }
        public List<ProductStock> Stocks { get; set; }
    }
}
