using AutoMapper;
using MassTransit;
using MediatR;
using SS_Microservice.Common.Logging.Messaging;
using SS_Microservice.Common.Types.Enums;
using SS_Microservice.Contracts.Events.Order;
using SS_Microservice.Contracts.Models;
using SS_Microservice.Services.Order.Application.Common.Constants;
using SS_Microservice.Services.Order.Application.Features.Order.Events;
using SS_Microservice.Services.Order.Application.Features.Order.Queries;
using SS_Microservice.Services.Order.Application.Interfaces;
using SS_Microservice.Services.Order.Application.Models.Order;

namespace SS_Microservice.Services.Order.Application.Features.Order.Commands
{
    public class UpdateOrderCommand : UpdateOrderRequest, IRequest<bool>
    {
    }

    public class UpdateOrderHandler : IRequestHandler<UpdateOrderCommand, bool>
    {
        private readonly IOrderService _orderService;
        private readonly ISender _sender;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<UpdateOrderHandler> _logger;
        private readonly string _handlerName = nameof(UpdateOrderHandler);

        public UpdateOrderHandler(IOrderService orderService, IMapper mapper,
            IPublishEndpoint publishEndpoint, ILogger<UpdateOrderHandler> logger, ISender sender)
        {
            _orderService = orderService;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
            _logger = logger;
            _sender = sender;
        }

        public async Task<bool> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var res = await _orderService.UpdateOrder(request);

            if (res)
            {
                var order = await _sender.Send(new GetOrderQuery()
                {
                    Id = request.OrderId
                });

                if (request.Status == ORDER_STATUS.CANCELLED)
                {

                    _logger.LogInformation(LoggerMessaging.StartPublishing(APPLICATION_SERVICE.ORDER_SERVICE,
                        nameof(IOrderCancelledEvent), _handlerName));

                    await _publishEndpoint.Publish<IOrderCancelledEvent>(new OrderCancelledEvent()
                    {
                        OrderId = request.OrderId,
                        Products = order.Items.Select(item => new ProductStock()
                        {
                            ProductId = item.ProductId,
                            VariantId = item.VariantId,
                            Quantity = item.Quantity
                        }).ToList()
                    });

                    _logger.LogInformation(LoggerMessaging.CompletePublishing(APPLICATION_SERVICE.ORDER_SERVICE,
                        nameof(IOrderCancelledEvent), _handlerName));
                }

                _logger.LogInformation(LoggerMessaging.StartPublishing(APPLICATION_SERVICE.ORDER_SERVICE,
                        nameof(IOrderStatusUpdatedEvent), _handlerName));

                await _publishEndpoint.Publish<IOrderStatusUpdatedEvent>(new OrderStatusUpdatedEvent()
                {
                    OrderCode = order.Code,
                    OrderId = order.Id,
                    Image = order.Items.FirstOrDefault().ProductImage,
                    UserId = order.User.Id,
                    Status = ORDER_STATUS.OrderStatusSubTitle[request.Status]
                });

                _logger.LogInformation(LoggerMessaging.CompletePublishing(APPLICATION_SERVICE.ORDER_SERVICE,
                    nameof(IOrderStatusUpdatedEvent), _handlerName));
            }

            return res;
        }
    }
}