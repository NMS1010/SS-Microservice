using SS_Microservice.Common.Types.Messages;

namespace SS_Microservice.Contracts.Commands.Product
{
    public interface IUpdateProductQuantityCommand : ICommand
    {
        long ProductId { get; set; }
        long Quantity { get; set; }
        long ActualInventory { get; set; }
    }
}
