using SS_Microservice.Common.Types.Messages;

namespace SS_Microservice.Contracts.Events.Order
{
    public interface IOrderCreationCompletedEvent : IEvent
    {
        string UserId { get; set; }
        string OrderCode { get; set; }
    }
}