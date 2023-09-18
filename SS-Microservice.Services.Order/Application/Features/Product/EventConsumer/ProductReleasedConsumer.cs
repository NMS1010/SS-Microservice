using MassTransit;
using MediatR;
using SS_Microservice.Common.Messages.Events.Product;
using SS_Microservice.Services.Order.Application.Features.Order.Commands;

namespace SS_Microservice.Services.Order.Application.Features.Product.EventConsumer
{
    public class ProductReleasedConsumer : IConsumer<ProductReleasedEvent>
    {
        private readonly ISender _sender;

        public ProductReleasedConsumer(ISender sender)
        {
            _sender = sender;
        }

        public async Task Consume(ConsumeContext<ProductReleasedEvent> context)
        {
            var res = await _sender.Send(new DeleteOrderCommand()
            {
                OrderId = context.Message.OrderId,
            });
        }
    }
}