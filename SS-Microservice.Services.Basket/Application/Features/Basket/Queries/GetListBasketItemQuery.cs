using AutoMapper;
using MediatR;
using SS_Microservice.Common.Exceptions;
using SS_Microservice.Common.Grpc.Product.Protos;
using SS_Microservice.Services.Basket.Application.Dto;
using SS_Microservice.Services.Basket.Application.Interfaces;

namespace SS_Microservice.Services.Basket.Application.Features.Basket.Queries
{
    public class GetListBasketItemQuery : IRequest<List<BasketItemDto>>
    {
        public List<long> Ids { get; set; }
        public string UserId { get; set; }
    }

    public class GetListBasketItemQueryHandler : IRequestHandler<GetListBasketItemQuery, List<BasketItemDto>>
    {
        private readonly IBasketService _basketService;
        private readonly IProductGrpcService _productGrpcService;
        private readonly IMapper _mapper;

        public GetListBasketItemQueryHandler(IBasketService basketService, IProductGrpcService productGrpcService, IMapper mapper)
        {
            _basketService = basketService;
            _productGrpcService = productGrpcService;
            _mapper = mapper;
        }

        public async Task<List<BasketItemDto>> Handle(GetListBasketItemQuery request, CancellationToken cancellationToken)
        {
            var basketItems = await _basketService.GetBasketItemByIds(request);

            foreach (var item in basketItems)
            {
                var product = await _productGrpcService.GetProductByVariantId(new GetProductByVariant() { VariantId = item.VariantId })
                    ?? throw new NotFoundException("Cannot find product with productId");

                _mapper.Map(item, product);
            }

            return basketItems;
        }
    }
}
