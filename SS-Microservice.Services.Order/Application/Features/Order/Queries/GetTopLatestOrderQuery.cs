using MediatR;
using SS_Microservice.Services.Order.Application.Dtos;
using SS_Microservice.Services.Order.Application.Interfaces;

namespace SS_Microservice.Services.Order.Application.Features.Order.Queries
{
    public class GetTopLatestOrderQuery : IRequest<List<OrderDto>>
    {
        public long Top { get; set; } = 5;
    }

    public class GetTopLatestOrderHandler : IRequestHandler<GetTopLatestOrderQuery, List<OrderDto>>
    {
        private readonly IOrderService _orderService;
        private readonly IProductGrpcService _productGrpcService;

        public GetTopLatestOrderHandler(IOrderService orderService, IProductGrpcService productGrpcService)
        {
            _orderService = orderService;
            _productGrpcService = productGrpcService;
        }

        public async Task<List<OrderDto>> Handle(GetTopLatestOrderQuery request, CancellationToken cancellationToken)
        {
            var order = await _orderService.GetTopOrderLatest(request);
            //order.OrderItems.ForEach(async oi =>
            //{
            //    var product = await _productGrpcService.GetProductByVariantId(oi.VariantId);
            //    oi.ProductImage = product.ProductImage;
            //});

            return order;
        }
    }
}
