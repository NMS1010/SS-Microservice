using SS_Microservice.Contracts.Events.User;

namespace SS_Microservice.Services.Auth.Application.Features.Auth.Events
{
    public class UserRegistedEvent : IUserRegistedEvent
    {
        public string UserId { get; set; }
        public string Email { get; set; }

        public Guid CorrelationId { get; set; }
    }
}
