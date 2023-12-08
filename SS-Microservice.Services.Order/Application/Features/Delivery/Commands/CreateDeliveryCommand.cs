using MediatR;
using SS_Microservice.Services.Order.Application.Interfaces;
using SS_Microservice.Services.Order.Application.Models.Delivery;

namespace SS_Microservice.Services.Order.Application.Features.Delivery.Commands
{
    public class CreateDeliveryCommand : CreateDeliveryRequest, IRequest<long>
    {
    }

    public class CreateDeliveryHandler : IRequestHandler<CreateDeliveryCommand, long>
    {
        private readonly IDeliveryService _deliveryService;

        public CreateDeliveryHandler(IDeliveryService deliveryService)
        {
            _deliveryService = deliveryService;
        }

        public async Task<long> Handle(CreateDeliveryCommand request, CancellationToken cancellationToken)
        {
            return await _deliveryService.CreateDelivery(request);
        }
    }
}