using AutoMapper;
using MassTransit;
using SS_Microservice.Common.Logging.Messaging;
using SS_Microservice.Common.Types.Enums;
using SS_Microservice.Contracts.Commands.Inventory;
using SS_Microservice.Contracts.Events.Inventory;
using SS_Microservice.Contracts.Models;
using SS_Microservice.Services.Inventory.Application.Interfaces;

namespace SS_Microservice.Services.Inventory.Infrastructure.Consumers.Commands.OrderingSaga
{
    public class ExportInventoryCommand : IExportInventoryCommand
    {
        public Guid CorrelationId { get; set; }
        public long OrderId { get; set; }
        public List<ProductStock> Stocks { get; set; }
    }

    class InventoryExportedEvent : IInventoryExportedEvent
    {
        public Guid CorrelationId { get; set; }
    }

    class InventoryExportationRejectedEvent : IInventoryExportationRejectedEvent
    {
        public Guid CorrelationId { get; set; }
    }


    public class ExportInventoryCommandConsumer : IConsumer<IExportInventoryCommand>
    {
        private readonly IMapper _mapper;
        private readonly IInventoryService _inventoryService;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<ExportInventoryCommandConsumer> _logger;
        private readonly string _handlerName = nameof(ExportInventoryCommandConsumer);

        public ExportInventoryCommandConsumer(IMapper mapper, IInventoryService inventoryService,
            IPublishEndpoint publishEndpoint, ILogger<ExportInventoryCommandConsumer> logger)
        {
            _mapper = mapper;
            _inventoryService = inventoryService;
            _publishEndpoint = publishEndpoint;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<IExportInventoryCommand> context)
        {
            var command = _mapper.Map<ExportInventoryCommand>(context.Message);
            var isSuccess = await _inventoryService.ExportInventory(command);

            var eventName = isSuccess ? nameof(IInventoryExportedEvent) : nameof(IInventoryExportationRejectedEvent);

            _logger.LogInformation(LoggerMessaging.StartPublishing(APPLICATION_SERVICE.INVENTORY_SERVICE,
                eventName, _handlerName));


            if (isSuccess)
            {
                await _publishEndpoint.Publish<IInventoryExportedEvent>(new InventoryExportedEvent()
                {
                    CorrelationId = context.Message.CorrelationId
                });
            }
            else
            {
                await _publishEndpoint.Publish<IInventoryExportationRejectedEvent>(new InventoryExportationRejectedEvent()
                {
                    CorrelationId = context.Message.CorrelationId
                });
            }

            _logger.LogInformation(LoggerMessaging.CompletePublishing(APPLICATION_SERVICE.INVENTORY_SERVICE,
                eventName, _handlerName));

        }
    }
}
