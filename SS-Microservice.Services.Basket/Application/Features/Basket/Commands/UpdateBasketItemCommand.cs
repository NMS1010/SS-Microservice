using MediatR;
using SS_Microservice.Common.Exceptions;
using SS_Microservice.Services.Basket.Application.Common.Enums;
using SS_Microservice.Services.Basket.Application.Interfaces;
using SS_Microservice.Services.Basket.Application.Model;

namespace SS_Microservice.Services.Basket.Application.Features.Basket.Commands
{
    public class UpdateBasketItemCommand : UpdateBasketItemRequest, IRequest<bool>
    {
    }

    public class UpdateBasketItemHandler : IRequestHandler<UpdateBasketItemCommand, bool>
    {
        private readonly IBasketService _basketService;
        private readonly IProductGrpcService _productGrpcService;

        public UpdateBasketItemHandler(IBasketService basketService, IProductGrpcService productGrpcService)
        {
            _basketService = basketService;
            _productGrpcService = productGrpcService;
        }

        public async Task<bool> Handle(UpdateBasketItemCommand request, CancellationToken cancellationToken)
        {
            var basketItems = await _basketService.GetBasketItemByIds(new Queries.GetListBasketItemQuery()
            {
                UserId = request.UserId,
                Ids = new List<long>() { request.CartItemId }
            });

            if (basketItems.Count == 0)
                throw new NotFoundException("Cannot find basket item");

            var variantId = basketItems[0].VariantId;

            var product = await _productGrpcService.GetProductByVariant(new SS_Microservice.Common.Grpc.Product.Protos.GetProductByVariant()
            {
                VariantId = variantId
            }) ?? throw new InvalidRequestException("Unexpected variantId");

            if (product.Status != PRODUCT_STATUS.ACTIVE)
                throw new InvalidRequestException("Unexpected variantId, product is not active");

            var quantity = product.ProductActualQuantity;
            if (quantity < (request.Quantity * product.VariantQuantity))
                throw new InvalidRequestException("Unexpected quantity, it must be less than or equal to product in inventory");

            return await _basketService.UpdateBasketItem(request);
        }
    }
}