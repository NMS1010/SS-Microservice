using SS_Microservice.Services.Inventory.Application.Dto;
using SS_Microservice.Services.Inventory.Application.Features.Docket.Commands;
using SS_Microservice.Services.Inventory.Application.Features.Docket.Queries;
using SS_Microservice.Services.Inventory.Infrastructure.Consumers.Commands.OrderingSaga;
using SS_Microservice.Services.Inventory.Infrastructure.Consumers.Events.Order;

namespace SS_Microservice.Services.Inventory.Application.Interfaces
{
    public interface IInventoryService
    {
        Task<long> ImportProduct(ImportProductCommand command);

        Task<List<DocketDto>> GetListDocketByProduct(GetListDocketQuery query);

        // messaging
        Task<bool> ExportInventory(ExportInventoryCommand command);

        Task RollBackInventory(RollBackInventoryCommand command);

        Task ImportInventory(ImportInventoryCommand command);
    }
}
