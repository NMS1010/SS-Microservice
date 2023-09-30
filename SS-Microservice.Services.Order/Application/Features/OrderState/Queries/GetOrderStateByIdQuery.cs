using MediatR;
using SS_Microservice.Services.Order.Application.Dtos;
using SS_Microservice.Services.Order.Application.Interfaces;

namespace SS_Microservice.Services.Order.Application.Features.OrderState.Queries
{
    public class GetOrderStateByIdQuery : IRequest<OrderStateDto>
    {
        public long OrderStateId { get; set; }
    }

    public class GetOrderStateByIdHandler : IRequestHandler<GetOrderStateByIdQuery, OrderStateDto>
    {
        private readonly IOrderStateService _orderStateService;

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