using MassTransit;
using MediatR;
using SS_Microservice.Contracts.Events.Basket;
using SS_Microservice.Services.Products.Application.Features.Product.Commands;

namespace SS_Microservice.Services.Products.Application.Messaging.Events.Basket
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
            var msg = context.Message;
            var isSuccess = await _sender.Send(new UpdateListProductQuantityCommand()
            {
                Products = msg.Products,
            });
        }
    }
}