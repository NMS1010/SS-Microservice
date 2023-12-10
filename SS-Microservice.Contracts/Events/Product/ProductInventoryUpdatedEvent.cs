using SS_Microservice.Contracts.Models;

namespace SS_Microservice.Contracts.Events.Product
{
    public class ProductInventoryUpdatedEvent
    {
        public long OrderId { get; set; }
        public string UserId { get; set; }
        public List<ProductStock> Products { get; set; }
    }
}