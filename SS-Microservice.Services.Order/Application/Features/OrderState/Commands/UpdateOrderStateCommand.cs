using MediatR;
using SS_Microservice.Services.Order.Application.Interfaces;
using SS_Microservice.Services.Order.Application.Models.OrderState;

namespace SS_Microservice.Services.Order.Application.Features.OrderState.Commands
{
    public class UpdateOrderStateCommand : UpdateOrderStateRequest, IRequest<bool>
    {
    }

    public class UpdateOrderStateHandler : IRequestHandler<UpdateOrderStateCommand, bool>
    {
        private readonly IOrderStateService _orderStateService;

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