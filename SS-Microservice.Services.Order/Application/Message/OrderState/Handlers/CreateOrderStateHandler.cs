using MediatR;
using SS_Microservice.Services.Order.Application.Message.OrderState.Commands;
using SS_Microservice.Services.Order.Core.Interfaces;

namespace SS_Microservice.Services.Order.Application.Message.OrderState.Handlers
{
    public class CreateOrderStateHandler : IRequestHandler<CreateOrderStateCommand>
    {
        private IOrderStateService _orderStateService;

        public CreateOrderStateHandler(IOrderStateService orderStateService)
        {
            _orderStateService = orderStateService;
        }

        public async Task Handle(CreateOrderStateCommand request, CancellationToken cancellationToken)
        {
            await _orderStateService.CreateOrderState(request);
        }
    }
}