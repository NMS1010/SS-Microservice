using MediatR;
using SS_Microservice.Common.Exceptions;
using SS_Microservice.Services.Basket.Application.Common.Enums;
using SS_Microservice.Services.Basket.Application.Interfaces;
using SS_Microservice.Services.Basket.Application.Model;

namespace SS_Microservice.Services.Basket.Application.Features.Basket.Commands
{
    public class CreateBasketItemCommand : CreateBasketItemRequest, IRequest<bool>
    {
        public long CurrentProductQuantity { get; set; }
        public long VariantQuantity { get; set; }
    }

    public class CreateBasketItemHandler : IRequestHandler<CreateBasketItemCommand, bool>
    {
        private readonly IBasketService _basketService;
        private readonly IProductGrpcService _productGrpcService;

        public CreateBasketItemHandler(IBasketService basketService, IProductGrpcService productGrpcService)
        {
            _basketService = basketService;
            _productGrpcService = productGrpcService;
        }

        public async Task<bool> Handle(CreateBasketItemCommand request, CancellationToken cancellationToken)
        {
            var product = await _productGrpcService.GetProductByVariant(new SS_Microservice.Common.Grpc.Product.Protos.GetProductByVariant()
            {
                VariantId = request.VariantId
            }) ?? throw new InvalidRequestException("Unexpected variantId");

            if (product.Status != PRODUCT_STATUS.ACTIVE)
                throw new InvalidRequestException("Unexpected variantId, product is not active");

            var quantity = product.ProductActualQuantity;
            if (quantity < (request.Quantity * product.VariantQuantity))
                throw new InvalidRequestException("Unexpected quantity, it must be less than or equal to product in inventory");
            request.CurrentProductQuantity = quantity;
            request.VariantQuantity = product.VariantQuantity;

            return await _basketService.CreateBasketItem(request);
        }
    }
}