using SS_Microservice.Common.Types.Messages;
using SS_Microservice.Contracts.Models;

namespace SS_Microservice.Contracts.Events.Order
{
    public interface IOrderCreatedEvent : IEvent
    {
        string UserId { get; set; }
        long OrderId { get; set; }
        string OrderCode { get; set; }
        decimal TotalPrice { get; set; }
        string PaymentMethod { get; set; }
        List<ProductStock> Products { get; set; }
    }
}