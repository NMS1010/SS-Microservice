using MassTransit;
using MediatR;
using SS_Microservice.Common.Messages.Events.Order;
using SS_Microservice.Common.Messages.Events.Product;
using SS_Microservice.Services.Products.Application.Message.Product.Commands;

namespace SS_Microservice.Services.Products.Application.Message.Order.Handler
{
    public class CreateOrderHandler : IConsumer<OrderCreatedEvent>
    {
        private readonly ISender _sender;
        private readonly IBus _publisher;

        public CreateOrderHandler(ISender sender, IBus publisher)
        {
            _sender = sender;
            _publisher = publisher;
        }

        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            var msg = context.Message;
            var isSuccess = await _sender.Send(new UpdateProductQuantityCommand()
            {
                Products = msg.Products,
            });
            if (isSuccess)
            {
                await _publisher.Publish(new ProductReservedEvent()
                {
                    OrderId = msg.OrderId,
                    UserId = msg.UserId,
                });
            }
            else
            {
                await _publisher.Publish(new ProductReservedRejectedEvent()
                {
                    OrderId = msg.OrderId
                });
            }
        }
    }
}