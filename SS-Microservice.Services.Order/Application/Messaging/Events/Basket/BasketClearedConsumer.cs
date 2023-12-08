using MassTransit;
using MediatR;
using SS_Microservice.Common.Messages.Events.Basket;

namespace SS_Microservice.Services.Order.Application.Messaging.Events.Basket
{
    public class BasketClearedConsumer : IConsumer<BasketClearedEvent>
    {
        private readonly ISender _sender;

        public BasketClearedConsumer(ISender sender)
        {
            _sender = sender;
        }

        public Task Consume(ConsumeContext<BasketClearedEvent> context)
        {
            throw new NotImplementedException();
        }
    }
}