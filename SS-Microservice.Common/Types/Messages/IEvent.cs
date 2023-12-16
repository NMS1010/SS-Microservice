using MassTransit;

namespace SS_Microservice.Common.Types.Messages
{
    public interface IEvent : CorrelatedBy<Guid>
    {
    }
}
