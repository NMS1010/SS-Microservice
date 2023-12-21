using MediatR;
using SS_Microservice.Services.Inventory.Application.Dto;
using SS_Microservice.Services.Inventory.Application.Interfaces;
using SS_Microservice.Services.Inventory.Application.Models.Docket;

namespace SS_Microservice.Services.Inventory.Application.Features.Docket.Queries
{
    public class GetListDocketByDateQuery : GetListDocketByDateRequest, IRequest<List<List<DocketDto>>>
    {
    }

    public class GetListDocketByDateHandler : IRequestHandler<GetListDocketByDateQuery, List<List<DocketDto>>>
    {
        private readonly IInventoryService _inventoryService;

        public GetListDocketByDateHandler(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        public async Task<List<List<DocketDto>>> Handle(GetListDocketByDateQuery request, CancellationToken cancellationToken)
        {
            return await _inventoryService.GetListDocketByDate(request);
        }
    }
}
