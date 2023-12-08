using MassTransit;
using MediatR;
using SS_Microservice.Common.Messages.Events.Product;
using SS_Microservice.Services.Order.Application.Features.Order.Commands;

namespace SS_Microservice.Services.Order.Application.Messaging.Events.Product
{
    public class ProductInventoryUpdatedRejectedConsumer : IConsumer<ProductInventoryUpdatedRejectedEvent>
    {
        private readonly ISender _sender;

        public ProductInventoryUpdatedRejectedConsumer(ISender sender)
        {
            _sender = sender;
        }

        public async Task Consume(ConsumeContext<ProductInventoryUpdatedRejectedEvent> context)
        {
            var res = await _sender.Send(new DeleteOrderCommand()
            {
                Id = context.Message.OrderId,
            });
        }
    }
}