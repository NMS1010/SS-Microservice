using MediatR;
using SS_Microservice.Services.Order.Application.Interfaces;
using SS_Microservice.Services.Order.Application.Models.OrderState;

namespace SS_Microservice.Services.Order.Application.Features.OrderState.Commands
{
    public class CreateOrderStateCommand : CreateOrderStateRequest, IRequest
    {
    }

    public class CreateOrderStateHandler : IRequestHandler<CreateOrderStateCommand>
    {
        private readonly IOrderStateService _orderStateService;

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