using MediatR;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Services.Order.Application.Dtos;
using SS_Microservice.Services.Order.Application.Message.Order.Queries;
using SS_Microservice.Services.Order.Core.Interfaces;

namespace SS_Microservice.Services.Order.Application.Message.Order.Handlers
{
    public class GetAllOrderHandler : IRequestHandler<GetAllOrderQuery, PaginatedResult<OrderDto>>
    {
        private readonly IOrderService _orderService;

        public GetAllOrderHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<PaginatedResult<OrderDto>> Handle(GetAllOrderQuery request, CancellationToken cancellationToken)
        {
            return await _orderService.GetOrderList(request);
        }
    }
}