using MassTransit;
using SS_Microservice.Contracts.Events.User;

namespace SS_Microservice.Services.Infrastructure.Application.Messaging.Events.User
{
    public class UserRegistedConsumer : IConsumer<IUserRegistedEvent>
    {
        public Task Consume(ConsumeContext<IUserRegistedEvent> context)
        {
            throw new NotImplementedException();
        }
    }
}