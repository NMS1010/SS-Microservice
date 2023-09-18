using MediatR;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Services.Order.Application.Dtos;
using SS_Microservice.Services.Order.Application.Features.OrderState.Queries;
using SS_Microservice.Services.Order.Application.Interfaces;

namespace SS_Microservice.Services.Order.Application.Features.OrderState.Handlers
{
    public class GetAllOrderStateHandler : IRequestHandler<GetAllOrderStateQuery, PaginatedResult<OrderStateDto>>
    {
        private IOrderStateService _orderStateService;

        public GetAllOrderStateHandler(IOrderStateService orderStateService)
        {
            _orderStateService = orderStateService;
        }

        public async Task<PaginatedResult<OrderStateDto>> Handle(GetAllOrderStateQuery request, CancellationToken cancellationToken)
        {
            return await _orderStateService.GetOrderStateList(request);
        }
    }
}