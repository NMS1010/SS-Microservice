using SS_Microservice.Contracts.Commands.Product;

namespace SS_Microservice.Services.Inventory.Application.Features.Product.Commands
{
    public class UpdateProductQuantityCommand : IUpdateProductQuantityCommand
    {
        public long ProductId { get; set; }
        public long Quantity { get; set; }
        public long ActualInventory { get; set; }
        public Guid CorrelationId { get; set; }
    }
}
