using MediatR;
using SS_Microservice.Services.Order.Application.Interfaces;
using SS_Microservice.Services.Order.Application.Models.Delivery;

namespace SS_Microservice.Services.Order.Application.Features.Delivery.Commands
{
    public class UpdateDeliveryCommand : UpdateDeliveryRequest, IRequest<bool>
    {
    }

    public class UpdateDeliveryHandler : IRequestHandler<UpdateDeliveryCommand, bool>
    {
        private readonly IDeliveryService _deliveryService;

        public UpdateDeliveryHandler(IDeliveryService deliveryService)
        {
            _deliveryService = deliveryService;
        }

        public async Task<bool> Handle(UpdateDeliveryCommand request, CancellationToken cancellationToken)
        {
            return await _deliveryService.UpdateDelivery(request);
        }
    }
}