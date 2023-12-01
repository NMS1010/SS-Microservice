using MediatR;
using SS_Microservice.Services.Inventory.Application.Dto;
using SS_Microservice.Services.Inventory.Application.Interfaces;

namespace SS_Microservice.Services.Inventory.Application.Features.Docket.Queries
{
    public class GetListDocketQuery : IRequest<List<DocketDto>>
    {
        public long ProductId { get; set; }
    }

    public class GetListDocketHandler : IRequestHandler<GetListDocketQuery, List<DocketDto>>
    {
        private readonly IInventoryService _inventoryService;

        public GetListDocketHandler(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        public async Task<List<DocketDto>> Handle(GetListDocketQuery request, CancellationToken cancellationToken)
        {
            return await _inventoryService.GetListDocketByProduct(request);
        }
    }
}
