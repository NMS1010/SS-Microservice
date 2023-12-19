using SS_Microservice.Contracts.Events.Order;
using SS_Microservice.Contracts.Models;

namespace SS_Microservice.Services.Order.Application.Features.Order.Events
{
    public class OrderCancelledEvent : IOrderCancelledEvent
    {
        public long OrderId { get; set; }
        public List<ProductStock> Products { get; set; }

        public Guid CorrelationId { get; set; }
    }
}
