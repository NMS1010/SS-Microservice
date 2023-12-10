using SS_Microservice.Contracts.Models;

namespace SS_Microservice.Contracts.Events.Basket
{
    public class BasketClearedRejectedEvent
    {
        public long OrderId { get; set; }

        public List<ProductStock> Products { get; set; }
    }
}