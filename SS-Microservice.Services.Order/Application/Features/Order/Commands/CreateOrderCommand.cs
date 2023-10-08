using MassTransit;
using MediatR;
using SS_Microservice.Common.Messages.Events.Order;
using SS_Microservice.Services.Order.Application.Interfaces;
using SS_Microservice.Services.Order.Application.Models.Order;

namespace SS_Microservice.Services.Order.Application.Features.Order.Commands
{
    public class CreateOrderCommand : CreateOrderRequest, IRequest<bool>
    {
        public string UserId { get; set; }
    }

    public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, bool>
    {
        private readonly IOrderService _orderService;
        private readonly IBus _publisher;
        private readonly ILogger<CreateOrderHandler> _logger;

        public CreateOrderHandler(IOrderService orderService, IBus publisher, ILogger<CreateOrderHandler> logger)
        {
            _orderService = orderService;
            _publisher = publisher;
            _logger = logger;
        }

        public async Task<bool> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var (isSuccess, orderId) = await _orderService.CreateOrder(request);
            if (isSuccess)
            {
                _logger.LogInformation($"Start publishing message to Product Service after created order");
                var e = new OrderCreatedEvent()
                {
                    UserId = request.UserId,
                    OrderId = orderId,
                    Products = new List<SS_Microservice.Common.Messages.Models.ProductStock>()
                };
                request.Items.ForEach(item =>
                {
                    e.Products.Add(new SS_Microservice.Common.Messages.Models.ProductStock()
                    {
                        ProductId = item.VariantId,
                        VariantId = item.VariantId,
                        Quantity = item.Quantity,
                    });
                });
                await _publisher.Publish(e);
                _logger.LogInformation($"Message to Product Service is published");
            }

            return isSuccess;
        }
    }
}