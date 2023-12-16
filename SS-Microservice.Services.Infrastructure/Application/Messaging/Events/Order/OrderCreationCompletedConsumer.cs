using MassTransit;
using SS_Microservice.Contracts.Events.Order;

namespace SS_Microservice.Services.Infrastructure.Application.Messaging.Events.Order
{
    public class OrderCreationCompletedConsumer : IConsumer<IOrderCreationCompletedEvent>
    {
        public Task Consume(ConsumeContext<IOrderCreationCompletedEvent> context)
        {
            throw new NotImplementedException();
        }
    }
}