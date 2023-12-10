using SS_Microservice.Contracts.Models;

namespace SS_Microservice.Contracts.Events.Order
{
    public class OrderCreatedEvent
    {
        public string UserId { get; set; }
        public long OrderId { get; set; }
        public List<ProductStock> Products { get; set; }
    }
}