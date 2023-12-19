using AutoMapper;
using MassTransit;
using MediatR;
using SS_Microservice.Common.Logging.Messaging;
using SS_Microservice.Common.RabbitMQ;
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
        private readonly ISendEndpointProvider _sendEndpoint;
        private readonly IMapper _mapper;
        private readonly string _handlerName = nameof(ImportProductHandler);

        public ImportProductHandler(IInventoryService inventoryService, ILogger<ImportProductHandler> logger, ISendEndpointProvider sendEndpoint, IMapper mapper)
        {
            _inventoryService = inventoryService;
            _logger = logger;
            _sendEndpoint = sendEndpoint;
            _mapper = mapper;
        }

        public async Task<bool> Handle(ImportProductCommand request, CancellationToken cancellationToken)
        {
            long docketId = await _inventoryService.ImportProduct(request);

            if (docketId > 0)
            {
                _logger.LogInformation(LoggerMessaging.StartPublishing(APPLICATION_SERVICE.INVENTORY_SERVICE,
                    nameof(IUpdateProductQuantityCommand), _handlerName));

                await (await _sendEndpoint.GetSendEndpoint(new Uri($"queue:{EventBusConstant.UpdateProductQuantity}")))
                    .Send<IUpdateProductQuantityCommand>(_mapper.Map<UpdateProductQuantityCommand>(request));

                _logger.LogInformation(LoggerMessaging.CompletePublishing(APPLICATION_SERVICE.INVENTORY_SERVICE,
                    nameof(IUpdateProductQuantityCommand), _handlerName));
            }


            return docketId > 0;
        }
    }
}
