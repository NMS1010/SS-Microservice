using AutoMapper;
using MassTransit;
using SS_Microservice.Contracts.Events.Order;
using SS_Microservice.Contracts.Models;
using SS_Microservice.Services.Inventory.Application.Interfaces;

namespace SS_Microservice.Services.Inventory.Infrastructure.Consumers.Events.Order
{
    public class ImportInventoryCommand
    {
        public long OrderId { get; set; }
        public List<ProductStock> Products { get; set; }
    }
    public class OrderCancelledEventConsumer : IConsumer<IOrderCancelledEvent>
    {
        private readonly IInventoryService _inventoryService;
        private readonly IMapper _mapper;

        public OrderCancelledEventConsumer(IInventoryService inventoryService, IMapper mapper)
        {
            _inventoryService = inventoryService;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<IOrderCancelledEvent> context)
        {
            await _inventoryService.ImportInventory(_mapper.Map<ImportInventoryCommand>(context.Message));
        }
    }
}
