using MediatR;
using SS_Microservice.Services.Order.Application.Interfaces;
using SS_Microservice.Services.Order.Application.Models.OrderCancellationReason;

namespace SS_Microservice.Services.Order.Application.Features.OrderCancellationReason.Commands
{
    public class CreateOrderCancellationReasonCommand : CreateOrderCancellationReasonRequest, IRequest<long>
    {
    }

    public class CreateOrderCancellationReasonHandler : IRequestHandler<CreateOrderCancellationReasonCommand, long>
    {
        private readonly IOrderCancellationReasonService _service;

        public CreateOrderCancellationReasonHandler(IOrderCancellationReasonService service)
        {
            _service = service;
        }

        public async Task<long> Handle(CreateOrderCancellationReasonCommand request, CancellationToken cancellationToken)
        {
            return await _service.CreateOrderCancellationReason(request);
        }
    }
}