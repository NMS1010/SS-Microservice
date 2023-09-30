using MediatR;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Services.Order.Application.Dtos;
using SS_Microservice.Services.Order.Application.Interfaces;

namespace SS_Microservice.Services.Order.Application.Features.Delivery.Queries
{
    public class GetDeliveryByIdQuery : IRequest<DeliveryDto>
    {
        public long Id { get; set; }
    }

    public class GetDeliveryByIdHandler : IRequestHandler<GetDeliveryByIdQuery, DeliveryDto>
    {
        private readonly IDeliveryService _deliveryService;

        public GetDeliveryByIdHandler(IDeliveryService deliveryService)
        {
            _deliveryService = deliveryService;
        }

        public async Task<DeliveryDto> Handle(GetDeliveryByIdQuery request, CancellationToken cancellationToken)
        {
            return await _deliveryService.GetDelivery(request);
        }
    }
}