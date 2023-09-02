using MediatR;
using SS_Microservice.Services.Order.Application.Message.Order.Commands;
using SS_Microservice.Services.Order.Core.Interfaces;

namespace SS_Microservice.Services.Order.Application.Message.Order.Handlers
{
    public class CreateOrderHandler : IRequestHandler<CreateOrderCommand>
    {
        private readonly IOrderService _orderService;

        public CreateOrderHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            await _orderService.CreateOrder(request);
        }
    }
}