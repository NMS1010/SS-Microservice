using MediatR;
using SS_Microservice.Services.Order.Application.Message.OrderState.Commands;
using SS_Microservice.Services.Order.Core.Interfaces;

namespace SS_Microservice.Services.Order.Application.Message.OrderState.Handlers
{
    public class DeleteOrderStateHandler : IRequestHandler<DeleteOrderStateCommand, bool>
    {
        private IOrderStateService _orderStateService;

        public DeleteOrderStateHandler(IOrderStateService orderStateService)
        {
            _orderStateService = orderStateService;
        }

        public async Task<bool> Handle(DeleteOrderStateCommand request, CancellationToken cancellationToken)
        {
            return await _orderStateService.DeleteOrderState(request);
        }
    }
}