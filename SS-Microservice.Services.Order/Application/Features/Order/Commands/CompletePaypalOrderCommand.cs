using AutoMapper;
using MassTransit;
using MediatR;
using SS_Microservice.Common.Logging.Messaging;
using SS_Microservice.Common.Types.Enums;
using SS_Microservice.Contracts.Events.Order;
using SS_Microservice.Services.Order.Application.Features.Order.Events;
using SS_Microservice.Services.Order.Application.Features.Order.Queries;
using SS_Microservice.Services.Order.Application.Interfaces;
using SS_Microservice.Services.Order.Application.Models.Order;

namespace SS_Microservice.Services.Order.Application.Features.Order.Commands
{
    public class CompletePaypalOrderCommand : CompletePaypalOrderRequest, IRequest<bool>
    {
    }

    public class CompletePaypalOrderHandler : IRequestHandler<CompletePaypalOrderCommand, bool>
    {
        private readonly IOrderService _orderService;
        private readonly ISender _sender;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<CompletePaypalOrderHandler> _logger;
        private readonly string _handlerName = nameof(CompletePaypalOrderHandler);

        public CompletePaypalOrderHandler(IOrderService orderService, ISender sender, IMapper mapper,
            IPublishEndpoint publishEndpoint, ILogger<CompletePaypalOrderHandler> logger)
        {
            _orderService = orderService;
            _sender = sender;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
            _logger = logger;
        }

        public async Task<bool> Handle(CompletePaypalOrderCommand request, CancellationToken cancellationToken)
        {
            var isSuccess = await _orderService.CompletePaypalOrder(request);
            if (isSuccess)
            {
                var order = await _sender.Send(new GetOrderQuery()
                {
                    Id = request.OrderId
                });

                _logger.LogInformation(LoggerMessaging.StartPublishing(APPLICATION_SERVICE.ORDER_SERVICE,
                        nameof(IOrderPaypalCompletedEvent), _handlerName));

                var address = order.Address;

                await _publishEndpoint.Publish<IOrderPaypalCompletedEvent>(new OrderPaypalCompletedEvent()
                {
                    OrderCode = order.Code,
                    OrderId = order.Id,
                    Image = order.Items.FirstOrDefault().ProductImage,
                    UserId = order.User.Id,
                    Address = $"{address.Street}, {address.Ward.Name}, {address.District.Name}, {address.Province.Name}",
                    Email = order.User.Email,
                    PaymentMethod = order.Transaction.PaymentMethod,
                    Phone = order.Address.Phone,
                    Receiver = order.Address.Receiver,
                    ReceiverEmail = order.Address.Email,
                    TotalPrice = order.Transaction.TotalPay,
                    UserName = order.User.FirstName + " " + order.User.LastName
                });

                _logger.LogInformation(LoggerMessaging.CompletePublishing(APPLICATION_SERVICE.ORDER_SERVICE,
                    nameof(IOrderPaypalCompletedEvent), _handlerName));
            }

            return isSuccess;
        }
    }
}
