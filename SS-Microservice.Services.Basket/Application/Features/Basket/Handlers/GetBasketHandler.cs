using MediatR;
using SS_Microservice.Common.Grpc.Product.Protos;
using SS_Microservice.Services.Basket.Application.Dto;
using SS_Microservice.Services.Basket.Application.Features.Basket.Queries;
using SS_Microservice.Services.Basket.Application.Interfaces;

namespace SS_Microservice.Services.Basket.Application.Features.Basket.Handlers
{
    public class GetBasketHandler : IRequestHandler<GetBasketQuery, BasketDto>
    {
        private readonly IBasketService _basketService;
        private readonly IProductGrpcService _productGrpcService;

        public GetBasketHandler(IBasketService basketService, IProductGrpcService productGrpcService)
        {
            _basketService = basketService;
            _productGrpcService = productGrpcService;
        }

        public async Task<BasketDto> Handle(GetBasketQuery request, CancellationToken cancellationToken)
        {
            var basket = await _basketService.GetBasket(request);
            var basketItems = basket.BasketItems.Items;
            foreach (var item in basketItems)
            {
                var product = await _productGrpcService.GetProductById(new GetProductDetail() { ProductId = item.ProductId });
                if (product != null)
                {
                    item.ProductName = product.Name;
                    item.ProductDescription = product.Description;
                    item.Price = (decimal)product.Price;
                    item.Quantity = product.Quantity;
                    item.MainImage = product.MainImage;
                    item.Origin = product.Origin;
                }
            }
            return basket;
        }
    }
}