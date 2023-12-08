using MediatR;
using SS_Microservice.Services.Order.Application.Dtos;
using SS_Microservice.Services.Order.Application.Interfaces;

namespace SS_Microservice.Services.Order.Application.Features.OrderCancellationReason.Queries
{
    public class GetOrderCancellationReasonQuery : IRequest<OrderCancellationReasonDto>
    {
        public long Id { get; set; }
    }

    public class GetOrderCancellationReasonHandler : IRequestHandler<GetOrderCancellationReasonQuery, OrderCancellationReasonDto>
    {
        private readonly IOrderCancellationReasonService _service;

        public GetOrderCancellationReasonHandler(IOrderCancellationReasonService service)
        {
            _service = service;
        }

        public async Task<OrderCancellationReasonDto> Handle(GetOrderCancellationReasonQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetOrderCancellationReason(request);
        }
    }
}