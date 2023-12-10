namespace SS_Microservice.Common.Messages.Events.User
{
    public interface IUserRegistedEvent : IEvent
    {
        string UserId { get; set; }
        string Email { get; set; }
    }
}