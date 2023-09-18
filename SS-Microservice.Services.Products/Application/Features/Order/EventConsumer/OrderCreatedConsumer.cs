using MassTransit;
using MediatR;
using SS_Microservice.Common.Messages.Events.Order;
using SS_Microservice.Common.Messages.Events.Product;
using SS_Microservice.Services.Products.Application.Features.Product.Commands;

namespace SS_Microservice.Services.Products.Application.Features.Order.EventConsumer
{
    public class OrderCreatedConsumer : IConsumer<OrderCreatedEvent>
    {
        private readonly ISender _sender;
        private readonly IBus _publisher;

        public OrderCreatedConsumer(ISender sender, IBus publisher)
        {
            _sender = sender;
            _publisher = publisher;
        }

        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            var msg = context.Message;

            msg.Products.ForEach(x => x.Quantity = -1 * x.Quantity);
            var isSuccess = await _sender.Send(new UpdateProductQuantityCommand()
            {
                Products = msg.Products,
            });
            if (isSuccess)
            {
                await _publisher.Publish(new ProductInventoryUpdatedEvent()
                {
                    OrderId = msg.OrderId,
                    UserId = msg.UserId,
                });
            }
            else
            {
                await _publisher.Publish(new ProductInventoryUpdatedRejectedEvent()
                {
                    OrderId = msg.OrderId
                });
            }
        }
    }
}