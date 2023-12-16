using SS_Microservice.Common.Types.Messages;

namespace SS_Microservice.Contracts.Commands.Inventory
{
    public interface IRollBackInventoryCommand : ICommand
    {
        long OrderId { get; set; }
    }
}
