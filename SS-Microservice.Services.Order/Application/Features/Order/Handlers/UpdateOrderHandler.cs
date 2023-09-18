using MediatR;
using SS_Microservice.Services.Order.Application.Features.Order.Commands;
using SS_Microservice.Services.Order.Application.Interfaces;

namespace SS_Microservice.Services.Order.Application.Features.Order.Handlers
{
    public class UpdateOrderHandler : IRequestHandler<UpdateOrderCommand, bool>
    {
        private readonly IOrderService _orderService;

        public UpdateOrderHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<bool> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            return await _orderService.UpdateOrder(request);
        }
    }
}