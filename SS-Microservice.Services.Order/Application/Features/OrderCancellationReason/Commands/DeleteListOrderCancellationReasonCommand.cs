using MediatR;
using SS_Microservice.Services.Order.Application.Interfaces;

namespace SS_Microservice.Services.Order.Application.Features.OrderCancellationReason.Commands
{
    public class DeleteListOrderCancellationReasonCommand : IRequest<bool>
    {
        public List<long> Ids { get; set; }
    }

    public class DeleteListOrderCancellationReasonHandler : IRequestHandler<DeleteListOrderCancellationReasonCommand, bool>
    {
        private readonly IOrderCancellationReasonService _service;

        public DeleteListOrderCancellationReasonHandler(IOrderCancellationReasonService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(DeleteListOrderCancellationReasonCommand request, CancellationToken cancellationToken)
        {
            return await _service.DeleteListOrderCancellationReason(request);
        }
    }
}
