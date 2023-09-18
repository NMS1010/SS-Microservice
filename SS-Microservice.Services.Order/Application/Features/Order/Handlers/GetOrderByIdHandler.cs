using MediatR;
using SS_Microservice.Services.Order.Application.Dtos;
using SS_Microservice.Services.Order.Application.Features.Order.Queries;
using SS_Microservice.Services.Order.Application.Interfaces;

namespace SS_Microservice.Services.Order.Application.Features.Order.Handlers
{
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
                var product = await _productGrpcService.GetProductById(oi.ProductId);
                oi.ProductImage = product.MainImage;
            });

            return order;
        }
    }
}