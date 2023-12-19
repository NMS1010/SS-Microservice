using SS_Microservice.Common.Types.Messages;

namespace SS_Microservice.Contracts.Events.Product
{
    public interface IStockReservedEvent : IEvent
    {
        string Image { get; set; }
    }
}