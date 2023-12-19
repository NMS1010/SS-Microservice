using MassTransit;
using SS_Microservice.Contracts.Events.User;

namespace SS_Microservice.Services.Infrastructure.Infrastructure.Consumers.Events.User
{
    public class UserRegistedEventConsumer : IConsumer<IUserRegistedEvent>
    {
        public Task Consume(ConsumeContext<IUserRegistedEvent> context)
        {
            throw new NotImplementedException();
        }
    }
}