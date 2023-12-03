using MediatR;
using SS_Microservice.Services.Order.Application.Dtos;
using SS_Microservice.Services.Order.Application.Interfaces;

namespace SS_Microservice.Services.Order.Application.Features.Order.Queries
{
    public class GetOrderByIdQuery : IRequest<OrderDto>
    {
        public long OrderId { get; set; }
        public string UserId { get; set; }
    }

    public class GetOrderByIdHandler : IRequestHandler<GetOrderByIdQuery, OrderDto>
    {
        private readonly IOrderService _orderService;
        private readonly IProductGrpcService _productGrpcService;

        public GetOrderByIdHandler(IOrderService orderService, IProductGrpcService productGrpcService)
        {
            _orderService = orderService;
            _productGrpcService = productGrpcService;
        }

        public async Task<OrderDto> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _orderService.GetOrder(request);
            order.OrderItems.ForEach(async oi =>
            {
                var product = await _productGrpcService.GetProductByVariantId(oi.VariantId);
                oi.ProductImage = product.ProductImage;
            });

            return order;
        }
    }
}