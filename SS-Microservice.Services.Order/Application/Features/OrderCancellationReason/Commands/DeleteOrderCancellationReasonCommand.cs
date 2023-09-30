using MediatR;
using SS_Microservice.Services.Order.Application.Interfaces;

namespace SS_Microservice.Services.Order.Application.Features.OrderCancellationReason.Commands
{
    public class DeleteOrderCancellationReasonCommand : IRequest<bool>
    {
        public long Id { get; set; }
    }

    public class DeleteOrderCancellationReasonHandler : IRequestHandler<DeleteOrderCancellationReasonCommand, bool>
    {
        private readonly IOrderCancellationReasonService _service;

        public DeleteOrderCancellationReasonHandler(IOrderCancellationReasonService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(DeleteOrderCancellationReasonCommand request, CancellationToken cancellationToken)
        {
            return await _service.DeleteOrderCancellationReason(request);
        }
    }
}