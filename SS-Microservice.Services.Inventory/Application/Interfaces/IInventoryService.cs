using SS_Microservice.Services.Inventory.Application.Dto;
using SS_Microservice.Services.Inventory.Application.Features.Docket.Commands;
using SS_Microservice.Services.Inventory.Application.Features.Docket.Queries;

namespace SS_Microservice.Services.Inventory.Application.Interfaces
{
    public interface IInventoryService
    {
        Task<bool> ImportProduct(ImportProductCommand command);

        Task<List<DocketDto>> GetListDocketByProduct(GetListDocketQuery query);
    }
}
