using MassTransit;
using MediatR;
using SS_Microservice.Common.Messages.Events.Basket;
using SS_Microservice.Common.Messages.Events.Product;
using SS_Microservice.Services.Products.Application.Message.Product.Commands;

namespace SS_Microservice.Services.Products.Application.Message.Basket.EventConsumer
{
    public class BasketClearedRejectedConsumer : IConsumer<BasketClearedRejectedEvent>
    {
        private readonly ISender _sender;
        private readonly IBus _publisher;

        public BasketClearedRejectedConsumer(ISender sender, IBus publisher)
        {
            _sender = sender;
            _publisher = publisher;
        }

        public async Task Consume(ConsumeContext<BasketClearedRejectedEvent> context)
        {
            var msg = context.Message;
            await _sender.Send(new UpdateProductQuantityCommand()
            {
                Products = msg.Products,
            });

            await _publisher.Publish(new ProductReleasedEvent()
            {
                OrderId = msg.OrderId,
            });
        }
    }
}