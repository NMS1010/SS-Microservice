using MassTransit;
using MediatR;
using SS_Microservice.Common.Exceptions;
using SS_Microservice.Common.Logging.Messaging;
using SS_Microservice.Common.Services.CurrentUser;
using SS_Microservice.Common.Types.Enums;
using SS_Microservice.Contracts.Events.Order;
using SS_Microservice.Contracts.Models;
using SS_Microservice.Services.Order.Application.Features.Order.Events;
using SS_Microservice.Services.Order.Application.Interfaces;
using SS_Microservice.Services.Order.Application.Models.Order;
using SS_Microservice.Services.Order.Infrastructure.Services.Address;

namespace SS_Microservice.Services.Order.Application.Features.Order.Commands
{
    public class CreateOrderCommand : CreateOrderRequest, IRequest<string>
    {
    }

    public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, string>
    {
        private readonly IOrderService _orderService;
        private readonly ICurrentUserService _currentUserService;

        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<CreateOrderHandler> _logger;
        private readonly IAddressClientAPI _addressClientAPI;
        private readonly IProductGrpcService _productGrpcService;
        private readonly string _handlerName = nameof(CreateOrderHandler);

        public CreateOrderHandler(IOrderService orderService, IPublishEndpoint publishEndpoint, ILogger<CreateOrderHandler> logger,
            IAddressClientAPI addressClientAPI, IProductGrpcService productGrpcService, ICurrentUserService currentUserService)
        {
            _orderService = orderService;
            _publishEndpoint = publishEndpoint;
            _logger = logger;
            _addressClientAPI = addressClientAPI;
            _productGrpcService = productGrpcService;
            _currentUserService = currentUserService;
        }

        public async Task<string> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var userAddressResp = await _addressClientAPI.GetDefaultAddress(request.UserId);
            if (userAddressResp == null || userAddressResp.Data == null)
            {
                throw new InternalServiceCommunicationException("Cannot get default user's address");
            }
            request.AddressId = userAddressResp.Data.Id;

            var productStocks = new List<ProductStock>();

            Dictionary<long, long> productActualQuantity = new();

            foreach (var item in request.Items)
            {
                var product = await _productGrpcService.GetProductByVariant(new SS_Microservice.Common.Grpc.Product.Protos.GetProductByVariant()
                {
                    VariantId = item.VariantId
                }) ?? throw new InternalServiceCommunicationException("Cannot get product by variant id");

                if (!productActualQuantity.ContainsKey(product.ProductId))
                {
                    productActualQuantity.Add(product.ProductId, product.ProductActualQuantity);
                }

                item.TotalPrice = (decimal)product.TotalPrice;

                var q = item.Quantity * product.VariantQuantity;

                if (q > productActualQuantity[product.ProductId])
                {
                    throw new InvalidRequestException("Unexpected quantity, it must be less than or equal to product in inventory");
                }

                productActualQuantity[product.ProductId] -= q;

                productStocks.Add(new ProductStock()
                {
                    ProductId = product.ProductId,
                    VariantId = product.VariantId,
                    Quantity = item.Quantity
                });
            }
            var resp = await _orderService.CreateOrder(request);

            if (resp != null && !string.IsNullOrEmpty(resp.OrderCode))
            {
                _logger.LogInformation(LoggerMessaging.StartPublishing(APPLICATION_SERVICE.ORDER_SERVICE,
                    nameof(CreateOrderCommand), _handlerName));

                var address = userAddressResp.Data;

                await _publishEndpoint.Publish<IOrderCreatedEvent>(new OrderCreatedEvent()
                {
                    UserId = request.UserId,
                    Email = _currentUserService.Email,
                    UserName = _currentUserService.UserName,
                    OrderCode = resp.OrderCode,
                    OrderId = resp.OrderId,
                    PaymentMethod = resp.PaymentMethod,
                    TotalPrice = resp.TotalPrice,
                    Products = productStocks,
                    Address = $"{address.Street}, {address?.Ward?.Name}, {address?.District?.Name}, {address?.Province?.Name}",
                    Phone = address.Phone,
                    Receiver = address.Receiver,
                    ReceiverEmail = address.Email,
                });

                _logger.LogInformation(LoggerMessaging.CompletePublishing(APPLICATION_SERVICE.ORDER_SERVICE,
                    nameof(CreateOrderCommand), _handlerName));
            }

            return resp.OrderCode;
        }
    }
}