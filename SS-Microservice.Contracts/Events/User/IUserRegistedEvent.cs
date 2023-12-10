using SS_Microservice.Common.Types.Messages;

namespace SS_Microservice.Contracts.Events.User
{
    public interface IUserRegistedEvent : IEvent
    {
        string UserId { get; set; }
        string Email { get; set; }
    }
}