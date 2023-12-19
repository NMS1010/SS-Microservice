using AutoMapper;
using MassTransit;
using SS_Microservice.Common.Logging.Messaging;
using SS_Microservice.Common.Types.Enums;
using SS_Microservice.Contracts.Commands.Product;
using SS_Microservice.Contracts.Events.Product;
using SS_Microservice.Contracts.Models;
using SS_Microservice.Services.Products.Application.Interfaces;

namespace SS_Microservice.Services.Products.Infrastructure.Consumers.Commands.OrderingSaga
{
    public class ReserveStockCommand : IReserveStockCommand
    {
        public List<ProductStock> Stocks { get; set; }

        public Guid CorrelationId { get; set; }
    }

    class StockReservationRejectedEvent : IStockReservationRejectedEvent
    {
        public Guid CorrelationId { get; set; }
    }

    class StockReservedEvent : IStockReservedEvent
    {
        public Guid CorrelationId { get; set; }
        public string Image { get; set; }
    }


    public class ReserveStockCommandConsumer : IConsumer<IReserveStockCommand>
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        private readonly ILogger<ReserveStockCommandConsumer> _logger;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly string _handlerName = nameof(ReserveStockCommandConsumer);

        public ReserveStockCommandConsumer(IProductService productService, IMapper mapper,
            ILogger<ReserveStockCommandConsumer> logger, IPublishEndpoint publishEndpoint)
        {
            _productService = productService;
            _mapper = mapper;
            _logger = logger;
            _publishEndpoint = publishEndpoint;
        }

        public async Task Consume(ConsumeContext<IReserveStockCommand> context)
        {
            var isSuccess = await _productService.ReserveStock(_mapper.Map<ReserveStockCommand>(context.Message));

            var eventName = isSuccess ? nameof(IStockReservedEvent) : nameof(IStockReservationRejectedEvent);

            _logger.LogInformation(LoggerMessaging.StartPublishing(APPLICATION_SERVICE.PRODUCT_SERVICE,
                eventName, _handlerName));


            if (isSuccess)
            {
                var product = await _productService.GetProduct(new Application.Features.Product.Queries.GetProductQuery()
                {
                    Id = context.Message.Stocks.First().ProductId
                });

                await _publishEndpoint.Publish<IStockReservedEvent>(new StockReservedEvent()
                {
                    CorrelationId = context.Message.CorrelationId,
                    Image = product?.Images?.First()?.Image
                });
            }
            else
            {
                await _publishEndpoint.Publish<IStockReservationRejectedEvent>(new StockReservationRejectedEvent()
                {
                    CorrelationId = context.Message.CorrelationId
                });
            }

            _logger.LogInformation(LoggerMessaging.CompletePublishing(APPLICATION_SERVICE.PRODUCT_SERVICE,
                eventName, _handlerName));
        }
    }
}
