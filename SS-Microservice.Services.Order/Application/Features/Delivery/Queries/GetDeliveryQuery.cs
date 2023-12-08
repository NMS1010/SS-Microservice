using MediatR;
using SS_Microservice.Services.Order.Application.Dtos;
using SS_Microservice.Services.Order.Application.Interfaces;

namespace SS_Microservice.Services.Order.Application.Features.Delivery.Queries
{
    public class GetDeliveryQuery : IRequest<DeliveryDto>
    {
        public long Id { get; set; }
    }

    public class GetDeliveryHandler : IRequestHandler<GetDeliveryQuery, DeliveryDto>
    {
        private readonly IDeliveryService _deliveryService;

        public GetDeliveryHandler(IDeliveryService deliveryService)
        {
            _deliveryService = deliveryService;
        }

        public async Task<DeliveryDto> Handle(GetDeliveryQuery request, CancellationToken cancellationToken)
        {
            return await _deliveryService.GetDelivery(request);
        }
    }
}