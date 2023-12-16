using SS_Microservice.Common.Types.Messages;

namespace SS_Microservice.Contracts.Events.Order
{
    public interface IOrderCreationRejectedEvent : IRejectedEvent
    {
        long OrderId { get; set; }
    }
}
