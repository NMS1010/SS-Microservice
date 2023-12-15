using SS_Microservice.Common.Grpc.Product.Protos;
using SS_Microservice.Common.Types.Enums;
using SS_Microservice.Services.Basket.Application.Interfaces;

namespace SS_Microservice.Services.Basket.Infrastructure.Services
{
    public class ProductGrpcService : IProductGrpcService
    {
        private readonly ProductProtoService.ProductProtoServiceClient _productProtoServiceClient;
        private readonly ILogger<ProductGrpcService> _logger;

        public ProductGrpcService(ProductProtoService.ProductProtoServiceClient productProtoServiceClient, ILogger<ProductGrpcService> logger)
        {
            _productProtoServiceClient = productProtoServiceClient;
            _logger = logger;
        }

        public async Task<ProductCustomGrpcResponse> GetProductByVariant(GetProductByVariant request)
        {
            _logger.LogInformation($"{APPLICATION_SERVICE.BASKET_SERVICE} Starting gRPC connection to {APPLICATION_SERVICE.PRODUCT_SERVICE}");
            var product = await _productProtoServiceClient.GetProductInformationAsync(request);
            if (product == null)
            {
                _logger.LogError($"{APPLICATION_SERVICE.BASKET_SERVICE} Failed to get product from {APPLICATION_SERVICE.PRODUCT_SERVICE}");
            }
            else
            {
                _logger.LogInformation($"{APPLICATION_SERVICE.BASKET_SERVICE} Get product successfully from {APPLICATION_SERVICE.PRODUCT_SERVICE}");
            }

            return product;
        }
    }
}