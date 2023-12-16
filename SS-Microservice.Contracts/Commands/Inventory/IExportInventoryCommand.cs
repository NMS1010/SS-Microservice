using SS_Microservice.Common.Types.Messages;
using SS_Microservice.Contracts.Models;

namespace SS_Microservice.Contracts.Commands.Inventory
{
    public interface IExportInventoryCommand : ICommand
    {
        long OrderId { get; set; }
        List<ProductStock> Stocks { get; set; }
    }
}
