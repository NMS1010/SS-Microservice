using MediatR;
using SS_Microservice.Services.Order.Application.Message.OrderState.Commands;
using SS_Microservice.Services.Order.Core.Interfaces;

namespace SS_Microservice.Services.Order.Application.Message.OrderState.Handlers
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