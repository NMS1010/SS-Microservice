using AutoMapper;
using MediatR;
using SS_Microservice.Services.Order.Application.Dtos;
using SS_Microservice.Services.Order.Application.Interfaces;

namespace SS_Microservice.Services.Order.Application.Features.Order.Queries
{
    public class GetOrderItemQuery : IRequest<OrderItemDto>
    {
        public long Id { get; set; }
    }

    public class GetOrderItemHandler : IRequestHandler<GetOrderItemQuery, OrderItemDto>
    {
        private readonly IOrderService _orderService;
        private readonly IProductGrpcService _productGrpcService;
        private readonly IMapper _mapper;

        public GetOrderItemHandler(IOrderService orderService, IProductGrpcService productGrpcService, IMapper mapper)
        {
            _orderService = orderService;
            _productGrpcService = productGrpcService;
            _mapper = mapper;
        }

        public async Task<OrderItemDto> Handle(GetOrderItemQuery request, CancellationToken cancellationToken)
        {
            var orderItem = await _orderService.GetOrderItem(request);
            var product = await _productGrpcService.GetProductByVariant(
                        new SS_Microservice.Common.Grpc.Product.Protos.GetProductByVariant()
                        {
                            VariantId = orderItem.VariantId
                        }
                    );
            _mapper.Map(product, orderItem);

            return orderItem;
        }
    }
}
