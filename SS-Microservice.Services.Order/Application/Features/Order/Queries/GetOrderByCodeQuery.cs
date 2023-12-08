using MediatR;
using SS_Microservice.Services.Order.Application.Dtos;
using SS_Microservice.Services.Order.Application.Interfaces;

namespace SS_Microservice.Services.Order.Application.Features.Order.Queries
{
    public class GetOrderByCodeQuery : IRequest<OrderDto>
    {
        public string UserId { get; set; }
        public string Code { get; set; }
    }

    public class GetOrderByCodeHandler : IRequestHandler<GetOrderByCodeQuery, OrderDto>
    {
        private readonly IOrderService _orderService;
        private readonly IProductGrpcService _productGrpcService;

        public GetOrderByCodeHandler(IOrderService orderService, IProductGrpcService productGrpcService)
        {
            _orderService = orderService;
            _productGrpcService = productGrpcService;
        }

        public async Task<OrderDto> Handle(GetOrderByCodeQuery request, CancellationToken cancellationToken)
        {
            var order = await _orderService.GetOrderByCode(request);
            //order.OrderItems.ForEach(async oi =>
            //{
            //    var product = await _productGrpcService.GetProductByVariantId(oi.VariantId);
            //    oi.ProductImage = product.ProductImage;
            //});

            return order;
        }
    }
}
