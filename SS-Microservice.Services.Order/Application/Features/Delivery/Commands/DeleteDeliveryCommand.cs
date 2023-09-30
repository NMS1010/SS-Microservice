using MediatR;
using SS_Microservice.Services.Order.Application.Interfaces;

namespace SS_Microservice.Services.Order.Application.Features.Delivery.Commands
{
    public class DeleteDeliveryCommand : IRequest<bool>
    {
        public long Id { get; set; }
    }

    public class DeleteDeliveryHandler : IRequestHandler<DeleteDeliveryCommand, bool>
    {
        private readonly IDeliveryService _deliveryService;

        public DeleteDeliveryHandler(IDeliveryService deliveryService)
        {
            _deliveryService = deliveryService;
        }

        public async Task<bool> Handle(DeleteDeliveryCommand request, CancellationToken cancellationToken)
        {
            return await _deliveryService.DeleteDelivery(request);
        }
    }
}