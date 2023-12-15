using AutoMapper;
using MediatR;
using SS_Microservice.Common.Exceptions;
using SS_Microservice.Common.Grpc.Product.Protos;
using SS_Microservice.Common.Types.Model.Paging;
using SS_Microservice.Services.Basket.Application.Dto;
using SS_Microservice.Services.Basket.Application.Interfaces;
using SS_Microservice.Services.Basket.Application.Model;

namespace SS_Microservice.Services.Basket.Application.Features.Basket.Queries
{
    public class GetListBasketByUserQuery : GetBasketPagingRequest, IRequest<PaginatedResult<BasketItemDto>>
    {
    }

    public class GetListBasketByUserHandler : IRequestHandler<GetListBasketByUserQuery, PaginatedResult<BasketItemDto>>
    {
        private readonly IBasketService _basketService;
        private readonly IProductGrpcService _productGrpcService;
        private readonly IMapper _mapper;

        public GetListBasketByUserHandler(IBasketService basketService, IProductGrpcService productGrpcService, IMapper mapper)
        {
            _basketService = basketService;
            _productGrpcService = productGrpcService;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<BasketItemDto>> Handle(GetListBasketByUserQuery request, CancellationToken cancellationToken)
        {
            var basketItems = await _basketService.GetBasketByUser(request);

            foreach (var item in basketItems.Items)
            {
                var product = await _productGrpcService.GetProductByVariant(new GetProductByVariant() { VariantId = item.VariantId })
                    ?? throw new NotFoundException("Cannot find product with productId");

                _mapper.Map(product, item);
            }

            return basketItems;
        }
    }
}