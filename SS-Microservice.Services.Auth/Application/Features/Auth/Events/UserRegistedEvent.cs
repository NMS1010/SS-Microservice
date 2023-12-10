using SS_Microservice.Common.Messages.Events.User;

namespace SS_Microservice.Services.Auth.Application.Features.Auth.Events
{
    public class UserRegistedEvent : IUserRegistedEvent
    {
        public string UserId { get; set; }
        public string Email { get; set; }
    }
}
