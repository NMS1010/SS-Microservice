using MediatR;
using SS_Microservice.Services.Order.Application.Interfaces;

namespace SS_Microservice.Services.Order.Application.Features.Delivery.Commands
{
    public class DeleteListDeliveryCommand : IRequest<bool>
    {
        public List<long> Ids { get; set; }
    }

    public class DeleteListDeliveryHandler : IRequestHandler<DeleteListDeliveryCommand, bool>
    {
        private readonly IDeliveryService _deliveryService;

        public DeleteListDeliveryHandler(IDeliveryService deliveryService)
        {
            _deliveryService = deliveryService;
        }

        public async Task<bool> Handle(DeleteListDeliveryCommand request, CancellationToken cancellationToken)
        {
            return await _deliveryService.DeleteListDelivery(request);
        }
    }
}
