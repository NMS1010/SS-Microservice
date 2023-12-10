namespace SS_Microservice.Contracts.Commands.Product
{
    public interface IUpdateProductQuantityCommand
    {
        public long ProductId { get; set; }
        public long Quantity { get; set; }
        public long ActualInventory { get; set; }
    }
}
