using MediatR;
using SS_Microservice.Services.Inventory.Application.Common.Constants;
using SS_Microservice.Services.Inventory.Application.Dto;
using SS_Microservice.Services.Inventory.Application.Interfaces;

namespace SS_Microservice.Services.Inventory.Application.Features.Docket.Queries
{
    public class GetListDocketByTypeQuery : IRequest<List<DocketDto>>
    {
        public string Type { get; set; } = DOCKET_TYPE.IMPORT;
    }

    public class GetListDocketByTypeHanlder : IRequestHandler<GetListDocketByTypeQuery, List<DocketDto>>
    {
        private readonly IInventoryService _inventoryService;

        public GetListDocketByTypeHanlder(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        public async Task<List<DocketDto>> Handle(GetListDocketByTypeQuery request, CancellationToken cancellationToken)
        {
            return await _inventoryService.GetListDocketByType(request);
        }
    }
}
