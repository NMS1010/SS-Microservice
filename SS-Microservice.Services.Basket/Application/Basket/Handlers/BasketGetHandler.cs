using MediatR;
using SS_Microservice.Common.Grpc.Product.Protos;
using SS_Microservice.Services.Basket.Application.Basket.Commands;
using SS_Microservice.Services.Basket.Application.Basket.Queries;
using SS_Microservice.Services.Basket.Application.Common.Interfaces;
using SS_Microservice.Services.Basket.Application.Dto;

namespace SS_Microservice.Services.Basket.Application.Basket.Handlers
{
    public class BasketGetHandler : IRequestHandler<BasketGetQuery, BasketDto>
    {
        private readonly IBasketService _basketService;
        private readonly IProductGrpcService _productGrpcService;

        public BasketGetHandler(IBasketService basketService, IProductGrpcService productGrpcService)
        {
            _basketService = basketService;
            _productGrpcService = productGrpcService;
        }

        public async Task<BasketDto> Handle(BasketGetQuery request, CancellationToken cancellationToken)
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