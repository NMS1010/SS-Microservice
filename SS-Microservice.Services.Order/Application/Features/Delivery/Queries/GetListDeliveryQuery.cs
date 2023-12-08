using MediatR;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Services.Order.Application.Dtos;
using SS_Microservice.Services.Order.Application.Interfaces;
using SS_Microservice.Services.Order.Application.Models.Delivery;

namespace SS_Microservice.Services.Order.Application.Features.Delivery.Queries
{
    public class GetListDeliveryQuery : GetDeliveryPagingRequest, IRequest<PaginatedResult<DeliveryDto>>
    {
    }

    public class GetListDeliveryHandler : IRequestHandler<GetListDeliveryQuery, PaginatedResult<DeliveryDto>>
    {
        private readonly IDeliveryService _deliveryService;

        public GetListDeliveryHandler(IDeliveryService deliveryService)
        {
            _deliveryService = deliveryService;
        }

        public async Task<PaginatedResult<DeliveryDto>> Handle(GetListDeliveryQuery request, CancellationToken cancellationToken)
        {
            return await _deliveryService.GetListDelivery(request);
        }
    }
}