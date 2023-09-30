using MediatR;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Services.Order.Application.Dtos;
using SS_Microservice.Services.Order.Application.Features.OrderCancellationReason.Commands;
using SS_Microservice.Services.Order.Application.Interfaces;

namespace SS_Microservice.Services.Order.Application.Features.OrderCancellationReason.Queries
{
    public class GetOrderCancellationReasonByIdQuery : IRequest<OrderCancellationReasonDto>
    {
        public long Id { get; set; }
    }

    public class GetOrderCancellationReasonByIdHandler : IRequestHandler<GetOrderCancellationReasonByIdQuery, OrderCancellationReasonDto>
    {
        private readonly IOrderCancellationReasonService _service;

        public GetOrderCancellationReasonByIdHandler(IOrderCancellationReasonService service)
        {
            _service = service;
        }

        public async Task<OrderCancellationReasonDto> Handle(GetOrderCancellationReasonByIdQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetOrderCancellationReason(request);
        }
    }
}