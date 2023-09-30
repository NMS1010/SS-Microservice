using MediatR;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Services.Order.Application.Dtos;
using SS_Microservice.Services.Order.Application.Features.Delivery.Commands;
using SS_Microservice.Services.Order.Application.Interfaces;
using SS_Microservice.Services.Order.Application.Models.Delivery;

namespace SS_Microservice.Services.Order.Application.Features.Delivery.Queries
{
    public class GetAllDeliveryQuery : GetDeliveryPagingRequest, IRequest<PaginatedResult<DeliveryDto>>
    {
    }

    public class GetAllDeliveryHandler : IRequestHandler<GetAllDeliveryQuery, PaginatedResult<DeliveryDto>>
    {
        private readonly IDeliveryService _deliveryService;

        public GetAllDeliveryHandler(IDeliveryService deliveryService)
        {
            _deliveryService = deliveryService;
        }

        public async Task<PaginatedResult<DeliveryDto>> Handle(GetAllDeliveryQuery request, CancellationToken cancellationToken)
        {
            return await _deliveryService.GetDeliveryList(request);
        }
    }
}