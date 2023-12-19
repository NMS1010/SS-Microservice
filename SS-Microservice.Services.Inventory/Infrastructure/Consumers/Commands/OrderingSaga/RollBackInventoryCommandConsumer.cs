using AutoMapper;
using MassTransit;
using SS_Microservice.Contracts.Commands.Inventory;
using SS_Microservice.Services.Inventory.Application.Interfaces;

namespace SS_Microservice.Services.Inventory.Infrastructure.Consumers.Commands.OrderingSaga
{
    public class RollBackInventoryCommand : IRollBackInventoryCommand
    {
        public Guid CorrelationId { get; set; }
        public long OrderId { get; set; }
    }
    public class RollBackInventoryCommandConsumer : IConsumer<IRollBackInventoryCommand>
    {
        private readonly IInventoryService _inventoryService;
        private readonly IMapper _mapper;

        public RollBackInventoryCommandConsumer(IInventoryService inventoryService, IMapper mapper)
        {
            _inventoryService = inventoryService;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<IRollBackInventoryCommand> context)
        {
            var command = _mapper.Map<RollBackInventoryCommand>(context.Message);

            await _inventoryService.RollBackInventory(command);
        }
    }
}
