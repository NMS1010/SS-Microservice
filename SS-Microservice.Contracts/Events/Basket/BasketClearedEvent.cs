using SS_Microservice.Common.Types.Messages;

namespace SS_Microservice.Contracts.Events.Basket
{
    public class BasketClearedEvent : IEvent
    {
        public long OrderId { get; set; }
    }
}