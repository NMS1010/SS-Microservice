using MediatR;
using SS_Microservice.Services.Order.Application.Interfaces;

namespace SS_Microservice.Services.Order.Application.Features.OrderState.Commands
{
    public class DeleteOrderStateCommand : IRequest<bool>
    {
        public long OrderStateId { get; set; }
    }

    public class DeleteOrderStateHandler : IRequestHandler<DeleteOrderStateCommand, bool>
    {
        private readonly IOrderStateService _orderStateService;

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