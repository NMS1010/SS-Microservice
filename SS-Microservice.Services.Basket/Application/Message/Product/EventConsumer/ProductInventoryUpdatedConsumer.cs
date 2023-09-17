using MassTransit;
using SS_Microservice.Common.Messages.Events.Basket;
using SS_Microservice.Common.Messages.Events.Product;
using SS_Microservice.Services.Basket.Application.Message.Basket.Commands;
using SS_Microservice.Services.Basket.Core.Interfaces;

namespace SS_Microservice.Services.Basket.Application.Message.Basket.EventConsumer
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
            var productIds = msg.Products.Select(x => x.ProductId).ToList();

            var isSuccess = await _basketService.ClearBasket(new ClearBasketCommand()
            {
                ProductIds = productIds,
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