using MediatR;
using SS_Microservice.Services.Inventory.Application.Interfaces;
using SS_Microservice.Services.Inventory.Application.Models.Inventory;

namespace SS_Microservice.Services.Inventory.Application.Features.Docket.Commands
{
    public class ImportProductCommand : ImportProductRequest, IRequest<bool>
    {
    }

    public class ImportProductHandler : IRequestHandler<ImportProductCommand, bool>
    {
        private readonly IInventoryService _inventoryService;

        public ImportProductHandler(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        public async Task<bool> Handle(ImportProductCommand request, CancellationToken cancellationToken)
        {
            long docketId = await _inventoryService.ImportProduct(request);

            //pub event to product service

            return docketId > 0;
        }
    }
}
