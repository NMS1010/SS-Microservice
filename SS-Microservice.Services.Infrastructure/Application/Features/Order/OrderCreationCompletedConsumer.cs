using MassTransit;
using SS_Microservice.Common.Messages.Events.Order;

namespace SS_Microservice.Services.Infrastructure.Application.Features.Order
{
    public class OrderCreationCompletedConsumer : IConsumer<OrderCreationCompletedEvent>
    {
        public Task Consume(ConsumeContext<OrderCreationCompletedEvent> context)
        {
            throw new NotImplementedException();
        }
    }
}