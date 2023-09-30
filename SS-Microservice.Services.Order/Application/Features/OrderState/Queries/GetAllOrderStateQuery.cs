using MediatR;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Services.Order.Application.Dtos;
using SS_Microservice.Services.Order.Application.Interfaces;
using SS_Microservice.Services.Order.Application.Models.Order;
using SS_Microservice.Services.Order.Application.Models.OrderState;

namespace SS_Microservice.Services.Order.Application.Features.OrderState.Queries
{
    public class GetAllOrderStateQuery : GetOrderStatePagingRequest, IRequest<PaginatedResult<OrderStateDto>>
    {
    }

    public class GetAllOrderStateHandler : IRequestHandler<GetAllOrderStateQuery, PaginatedResult<OrderStateDto>>
    {
        private readonly IOrderStateService _orderStateService;

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