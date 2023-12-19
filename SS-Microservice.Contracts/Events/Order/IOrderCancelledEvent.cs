using SS_Microservice.Common.Types.Messages;
using SS_Microservice.Contracts.Models;

namespace SS_Microservice.Contracts.Events.Order
{
    public interface IOrderCancelledEvent : IEvent
    {
        long OrderId { get; set; }
        List<ProductStock> Products { get; set; }
    }
}
