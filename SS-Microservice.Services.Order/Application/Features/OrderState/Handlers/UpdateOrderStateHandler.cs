using MediatR;
using SS_Microservice.Services.Order.Application.Features.OrderState.Commands;
using SS_Microservice.Services.Order.Application.Interfaces;

namespace SS_Microservice.Services.Order.Application.Features.OrderState.Handlers
{
    public class UpdateOrderStateHandler : IRequestHandler<UpdateOrderStateCommand, bool>
    {
        private IOrderStateService _orderStateService;

        public UpdateOrderStateHandler(IOrderStateService orderStateService)
        {
            _orderStateService = orderStateService;
        }

        public async Task<bool> Handle(UpdateOrderStateCommand request, CancellationToken cancellationToken)
        {
            return await _orderStateService.UpdateOrderState(request);
        }
    }
}