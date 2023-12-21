using SS_Microservice.Common.Grpc.Product.Protos;
using SS_Microservice.Common.Types.Enums;
using SS_Microservice.Services.Order.Application.Interfaces;

namespace SS_Microservice.Services.Order.Infrastructure.Services
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

        public async Task<ProductCustomGrpcResponse> GetProductById(GetProductById request)
        {
            _logger.LogInformation($"{APPLICATION_SERVICE.ORDER_SERVICE} Starting gRPC connection to Product Service");
            var product = await _productProtoServiceClient.GetProductInformationByIdAsync(request);
            if (product == null)
            {
                _logger.LogError($"{APPLICATION_SERVICE.ORDER_SERVICE} Failed to get product from gRPC service");
            }
            else
            {
                _logger.LogInformation($"{APPLICATION_SERVICE.ORDER_SERVICE} Get product successfully from gRPC service");
            }

            return product;
        }

        public async Task<ProductCustomGrpcResponse> GetProductByVariant(GetProductByVariant request)
        {
            _logger.LogInformation($"{APPLICATION_SERVICE.ORDER_SERVICE} Starting gRPC connection to Product Service");
            var product = await _productProtoServiceClient.GetProductInformationAsync(request);
            if (product == null)
            {
                _logger.LogError($"{APPLICATION_SERVICE.ORDER_SERVICE} Failed to get product from gRPC service");
            }
            else
            {
                _logger.LogInformation($"{APPLICATION_SERVICE.ORDER_SERVICE} Get product successfully from gRPC service");
            }

            return product;
        }
    }
}