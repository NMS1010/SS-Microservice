using SS_Microservice.Common.Grpc.Product.Protos;
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

        public async Task<ProductCustomGrpcResponse> GetProductByVariantId(GetProductByVariant request)
        {
            _logger.LogInformation("[Basket Service] Starting gRPC connection to Product Service");
            var product = await _productProtoServiceClient.GetProductInformationAsync(request);
            if (product == null)
            {
                _logger.LogError("[Basket Service] Failed to get product from gRPC service");
            }
            else
            {
                _logger.LogInformation("[Basket Service] Get product successfully from gRPC service");
            }

            return product;
        }
    }
}