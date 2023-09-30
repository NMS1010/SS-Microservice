using MediatR;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Services.Order.Application.Dtos;
using SS_Microservice.Services.Order.Application.Features.OrderCancellationReason.Commands;
using SS_Microservice.Services.Order.Application.Interfaces;
using SS_Microservice.Services.Order.Application.Models.OrderCancellationReason;

namespace SS_Microservice.Services.Order.Application.Features.OrderCancellationReason.Queries
{
    public class GetAllOrderCancellationReasonQuery : GetOrderCancellationReasonPagingRequest, IRequest<PaginatedResult<OrderCancellationReasonDto>>
    {
    }

    public class GetAllOrderCancellationReasonHandler : IRequestHandler<GetAllOrderCancellationReasonQuery, PaginatedResult<OrderCancellationReasonDto>>
    {
        private readonly IOrderCancellationReasonService _service;

        public GetAllOrderCancellationReasonHandler(IOrderCancellationReasonService service)
        {
            _service = service;
        }

        public async Task<PaginatedResult<OrderCancellationReasonDto>> Handle(GetAllOrderCancellationReasonQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetOrderCancellationReasonList(request);
        }
    }
}