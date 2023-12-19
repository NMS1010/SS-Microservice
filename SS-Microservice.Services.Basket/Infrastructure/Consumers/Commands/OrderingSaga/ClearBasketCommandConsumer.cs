using AutoMapper;
using MassTransit;
using SS_Microservice.Common.Logging.Messaging;
using SS_Microservice.Common.Types.Enums;
using SS_Microservice.Contracts.Commands.Basket;
using SS_Microservice.Contracts.Events.Basket;
using SS_Microservice.Services.Basket.Application.Interfaces;

namespace SS_Microservice.Services.Basket.Infrastructure.Consumers.Commands.OrderingSaga
{
    public class ClearBasketCommand : IClearBasketCommand
    {
        public string UserId { get; set; }
        public List<long> VariantIds { get; set; }

        public Guid CorrelationId { get; set; }
    }

    class BasketClearedRejectedEvent : IBasketClearedRejectedEvent
    {
        public Guid CorrelationId { get; set; }
    }

    class BasketClearedEvent : IBasketClearedEvent
    {
        public Guid CorrelationId { get; set; }
    }

    public class ClearBasketCommandConsumer : IConsumer<IClearBasketCommand>
    {
        private readonly IBasketService _basketService;
        private readonly IMapper _mapper;
        private readonly ILogger<ClearBasketCommandConsumer> _logger;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly string _handlerName = nameof(ClearBasketCommandConsumer);

        public ClearBasketCommandConsumer(IBasketService basketService, IMapper mapper,
            ILogger<ClearBasketCommandConsumer> logger, IPublishEndpoint publishEndpoint)
        {
            _basketService = basketService;
            _mapper = mapper;
            _logger = logger;
            _publishEndpoint = publishEndpoint;
        }

        public async Task Consume(ConsumeContext<IClearBasketCommand> context)
        {
            var isSuccess = await _basketService.ClearBasket(_mapper.Map<ClearBasketCommand>(context.Message));

            var eventName = isSuccess ? nameof(IBasketClearedEvent) : nameof(IBasketClearedRejectedEvent);

            _logger.LogInformation(LoggerMessaging.StartPublishing(APPLICATION_SERVICE.BASKET_SERVICE,
                eventName, _handlerName));


            if (isSuccess)
            {
                await _publishEndpoint.Publish<IBasketClearedEvent>(new BasketClearedEvent()
                {
                    CorrelationId = context.Message.CorrelationId
                });
            }
            else
            {
                await _publishEndpoint.Publish<IBasketClearedRejectedEvent>(new BasketClearedRejectedEvent()
                {
                    CorrelationId = context.Message.CorrelationId
                });
            }

            _logger.LogInformation(LoggerMessaging.CompletePublishing(APPLICATION_SERVICE.BASKET_SERVICE,
                eventName, _handlerName));
        }
    }
}
