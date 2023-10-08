using MassTransit;
using SS_Microservice.Common.Messages.Events.Basket;
using SS_Microservice.Common.Messages.Events.Product;
using SS_Microservice.Services.Basket.Application.Interfaces;
using SS_Microservice.Services.Basket.Application.Features.Basket.Commands;

namespace SS_Microservice.Services.Basket.Application.Features.Product.EventConsumer
{
    public class ProductInventoryUpdatedConsumer : IConsumer<ProductInventoryUpdatedEvent>
    {
        private readonly IBasketService _basketService;
        private readonly IBus _publisher;

        public ProductInventoryUpdatedConsumer(IBasketService basketService, IBus publisher)
        {
            _basketService = basketService;
            _publisher = publisher;
        }

        public async Task Consume(ConsumeContext<ProductInventoryUpdatedEvent> context)
        {
            var msg = context.Message;
            var variantIds = msg.Products.Select(x => x.VariantId).ToList();

            var isSuccess = await _basketService.ClearBasket(new ClearBasketCommand()
            {
                VariantIds = variantIds,
                UserId = msg.UserId
            });

            if (isSuccess)
            {
                await _publisher.Publish(new BasketClearedEvent()
                {
                    OrderId = msg.OrderId
                });
            }
            else
            {
                await _publisher.Publish(new BasketClearedRejectedEvent()
                {
                    OrderId = msg.OrderId,
                    Products = msg.Products
                });
            }
        }
    }
}