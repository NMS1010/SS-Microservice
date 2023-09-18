using MediatR;
using SS_Microservice.Services.Order.Application.Dtos;
using SS_Microservice.Services.Order.Application.Features.OrderState.Queries;
using SS_Microservice.Services.Order.Application.Interfaces;

namespace SS_Microservice.Services.Order.Application.Features.OrderState.Handlers
{
    public class GetOrderStateByIdHandler : IRequestHandler<GetOrderStateByIdQuery, OrderStateDto>
    {
        private IOrderStateService _orderStateService;

        public GetOrderStateByIdHandler(IOrderStateService orderStateService)
        {
            _orderStateService = orderStateService;
        }

        public async Task<OrderStateDto> Handle(GetOrderStateByIdQuery request, CancellationToken cancellationToken)
        {
            return await _orderStateService.GetOrderState(request);
        }
    }
}