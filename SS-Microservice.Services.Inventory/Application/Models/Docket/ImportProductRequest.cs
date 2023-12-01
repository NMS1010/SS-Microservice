namespace SS_Microservice.Services.Inventory.Application.Models.Inventory
{
    public class ImportProductRequest
    {
        public long ProductId { get; set; }
        public long Quantity { get; set; }
        public long ActualInventory { get; set; }
        public string Note { get; set; }
    }
}
