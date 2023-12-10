using AutoMapper;
using MassTransit;
using MediatR;
using SS_Microservice.Common.Logging.Messaging;
using SS_Microservice.Common.Types.Enums;
using SS_Microservice.Contracts.Commands.Product;
using SS_Microservice.Services.Inventory.Application.Features.Product.Commands;
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
        private readonly ILogger<ImportProductHandler> _logger;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IMapper _mapper;
        private readonly string _handlerName = nameof(ImportProductHandler);

        public ImportProductHandler(IInventoryService inventoryService, ILogger<ImportProductHandler> logger, IPublishEndpoint publishEndpoint, IMapper mapper)
        {
            _inventoryService = inventoryService;
            _logger = logger;
            _publishEndpoint = publishEndpoint;
            _mapper = mapper;
        }

        public async Task<bool> Handle(ImportProductCommand request, CancellationToken cancellationToken)
        {
            long docketId = await _inventoryService.ImportProduct(request);

            if (docketId > 0)
            {
                _logger.LogInformation(LoggerMessaging.StartPublishing(APPLICATION_SERVICE.INVENTORY_SERVICE,
                    nameof(ImportProductCommand), _handlerName));

                await _publishEndpoint.Publish<IUpdateProductQuantityCommand>(_mapper.Map<UpdateProductQuantityCommand>(request),
                    cancellationToken);

                _logger.LogInformation(LoggerMessaging.CompletePublishing(APPLICATION_SERVICE.INVENTORY_SERVICE,
                    nameof(ImportProductCommand), _handlerName));
            }


            return docketId > 0;
        }
    }
}
