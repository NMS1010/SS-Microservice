using MassTransit;
using SS_Microservice.Contracts.Events.Order;
using SS_Microservice.Services.Order.Application.Common.Constants;
using SS_Microservice.Services.Order.Application.Features.Order.Commands;
using SS_Microservice.Services.Order.Application.Interfaces;

namespace SS_Microservice.Services.Order.Infrastructure.Consumers.Events.OrderingSaga
{
    public class OrderCreationCompletedEventConsumer : IConsumer<IOrderCreationCompletedEvent>
    {
        private readonly IOrderService _orderService;
        private readonly ILogger<OrderCreationCompletedEventConsumer> _logger;

        public OrderCreationCompletedEventConsumer(IOrderService orderService, ILogger<OrderCreationCompletedEventConsumer> logger)
        {
            _orderService = orderService;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<IOrderCreationCompletedEvent> context)
        {
            _logger.LogInformation($"OrderCreationCompletedEventConsumer: received event, orderId = {context.Message.OrderId}");
            var command = new UpdateOrderCommand()
            {
                OrderId = context.Message.OrderId,
                UserId = context.Message.UserId,
                Status = ORDER_STATUS.NOT_PROCESSED
            };
            await _orderService.UpdateOrder(command);
        }
    }
}
