using MassTransit;
using MediatR;
using SS_Microservice.Contracts.Events.Basket;
using SS_Microservice.Services.Order.Application.Features.Order.Commands;

namespace SS_Microservice.Services.Order.Application.Messaging.Events.Basket
{
    public class BasketClearedRejectedConsumer : IConsumer<BasketClearedRejectedEvent>
    {
        private readonly ISender _sender;

        public BasketClearedRejectedConsumer(ISender sender)
        {
            _sender = sender;
        }

        public async Task Consume(ConsumeContext<BasketClearedRejectedEvent> context)
        {
            var res = await _sender.Send(new DeleteOrderCommand()
            {
                Id = context.Message.OrderId,
            });
        }
    }
}