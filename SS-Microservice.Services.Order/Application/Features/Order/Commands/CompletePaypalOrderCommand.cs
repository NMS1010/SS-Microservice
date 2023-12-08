using MassTransit;
using MediatR;
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
        private readonly IBus _publisher;
        private readonly ILogger<CompletePaypalOrderHandler> _logger;

        public CompletePaypalOrderHandler(IOrderService orderService, IBus publisher, ILogger<CompletePaypalOrderHandler> logger)
        {
            _orderService = orderService;
            _publisher = publisher;
            _logger = logger;
        }

        public async Task<bool> Handle(CompletePaypalOrderCommand request, CancellationToken cancellationToken)
        {
            var isSuccess = await _orderService.CompletePaypalOrder(request);
            //if (isSuccess)
            //{
            //    _logger.LogInformation($"Start publishing message to Product Service after created order");
            //    var e = new OrderCreatedEvent()
            //    {
            //        UserId = request.UserId,
            //        OrderId = orderId,
            //        Products = new List<SS_Microservice.Common.Messages.Models.ProductStock>()
            //    };
            //    request.Items.ForEach(item =>
            //    {
            //        e.Products.Add(new SS_Microservice.Common.Messages.Models.ProductStock()
            //        {
            //            ProductId = item.VariantId,
            //            VariantId = item.VariantId,
            //            Quantity = item.Quantity,
            //        });
            //    });
            //    await _publisher.Publish(e);
            //    _logger.LogInformation($"Message to Product Service is published");
            //}

            //return isSuccess;

            return isSuccess;
        }
    }
}
