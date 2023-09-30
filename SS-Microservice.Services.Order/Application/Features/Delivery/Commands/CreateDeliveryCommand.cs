using MediatR;
using SS_Microservice.Services.Order.Application.Interfaces;
using SS_Microservice.Services.Order.Application.Models.Delivery;

namespace SS_Microservice.Services.Order.Application.Features.Delivery.Commands
{
    public class CreateDeliveryCommand : CreateDeliveryRequest, IRequest<bool>
    {
    }

    public class CreateDeliveryHandler : IRequestHandler<CreateDeliveryCommand, bool>
    {
        private readonly IDeliveryService _deliveryService;

        public CreateDeliveryHandler(IDeliveryService deliveryService)
        {
            _deliveryService = deliveryService;
        }

        public async Task<bool> Handle(CreateDeliveryCommand request, CancellationToken cancellationToken)
        {
            return await _deliveryService.CreateDelivery(request);
        }
    }
}