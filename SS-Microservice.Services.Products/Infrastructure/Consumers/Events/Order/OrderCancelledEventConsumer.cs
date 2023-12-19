using MassTransit;
using SS_Microservice.Contracts.Events.Order;
using SS_Microservice.Services.Products.Application.Interfaces;

namespace SS_Microservice.Services.Products.Infrastructure.Consumers.Events.Order
{
    public class OrderCancelledEventConsumer : IConsumer<IOrderCancelledEvent>
    {
        private readonly IProductService _productService;

        public OrderCancelledEventConsumer(IProductService productService)
        {
            _productService = productService;
        }

        public async Task Consume(ConsumeContext<IOrderCancelledEvent> context)
        {
            await _productService.ReserveStock(new Commands.OrderingSaga.ReserveStockCommand()
            {
                Stocks = context.Message.Products.Select(x => new Contracts.Models.ProductStock()
                {
                    ProductId = x.ProductId,
                    VariantId = x.VariantId,
                    Quantity = -1 * x.Quantity
                }).ToList()
            });
        }
    }
}
