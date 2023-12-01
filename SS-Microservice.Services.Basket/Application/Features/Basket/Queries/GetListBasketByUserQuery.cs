using MediatR;
using SS_Microservice.Common.Model.Paging;
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

        public GetListBasketByUserHandler(IBasketService basketService, IProductGrpcService productGrpcService)
        {
            _basketService = basketService;
            _productGrpcService = productGrpcService;
        }

        public async Task<PaginatedResult<BasketItemDto>> Handle(GetListBasketByUserQuery request, CancellationToken cancellationToken)
        {
            var basketItems = await _basketService.GetBasketByUser(request);
            //foreach (var item in basketItems)
            //{
            //    var product = await _productGrpcService.GetProductByVariantId(new GetProductDetailByVariant() { VariantId = item.VariantId });
            //    if (product != null)
            //    {
            //        item.ProductName = product.Name;
            //        item.ProductUnit = product.Unit;
            //        item.ProductId = product.ProductId;
            //        item.VariantName = product.VariantName;
            //        item.VariantQuantity = product.VariantQuantity;
            //        item.Price = (decimal)product.Price;
            //        item.PromotionalPrice = (decimal)product.PromotionalPrice;
            //        item.DefaultImage = product.Image;
            //        item.CategoryName = product.CategoryName;
            //        item.CategoryName = product.CategorySlug;
            //        item.BrandName = product.BrandName;
            //        item.Status = product.Status;
            //        item.Rating = product.Rating;
            //        item.Slug = product.Slug;
            //    }
            //}
            return basketItems;
        }
    }
}