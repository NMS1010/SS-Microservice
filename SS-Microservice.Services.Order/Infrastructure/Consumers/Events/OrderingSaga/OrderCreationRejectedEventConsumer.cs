using MassTransit;
using SS_Microservice.Contracts.Events.Order;
using SS_Microservice.Services.Order.Application.Interfaces;

namespace SS_Microservice.Services.Order.Infrastructure.Consumers.Events.OrderingSaga
{
    public class DeleteOrderCommand
    {
        public long Id { get; set; }
    }

    public class OrderCreationRejectedEventConsumer : IConsumer<IOrderCreationRejectedEvent>
    {
        private readonly IOrderService _orderService;

        public OrderCreationRejectedEventConsumer(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task Consume(ConsumeContext<IOrderCreationRejectedEvent> context)
        {
            await _orderService.DeleteOrder(new DeleteOrderCommand { Id = context.Message.OrderId });
        }
    }
}
