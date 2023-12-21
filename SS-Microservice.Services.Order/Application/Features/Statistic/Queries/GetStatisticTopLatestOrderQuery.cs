using AutoMapper;
using MediatR;
using SS_Microservice.Services.Order.Application.Dtos;
using SS_Microservice.Services.Order.Application.Interfaces;

namespace SS_Microservice.Services.Order.Application.Features.Statistic.Queries
{
    public class GetStatisticTopLatestOrderQuery : IRequest<List<OrderDto>>
    {
        public int Top { get; set; } = 5;
    }

    public class GetStatisticTopLatestOrderHandler : IRequestHandler<GetStatisticTopLatestOrderQuery, List<OrderDto>>
    {
        private readonly IOrderService _orderService;
        private readonly IProductGrpcService _productGrpcService;
        private readonly IMapper _mapper;

        public GetStatisticTopLatestOrderHandler(IOrderService orderService, IProductGrpcService productGrpcService, IMapper mapper)
        {
            _orderService = orderService;
            _productGrpcService = productGrpcService;
            _mapper = mapper;
        }

        public async Task<List<OrderDto>> Handle(GetStatisticTopLatestOrderQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderService.GetTopOrderLatest(request);

            return orders;
        }
    }
}
