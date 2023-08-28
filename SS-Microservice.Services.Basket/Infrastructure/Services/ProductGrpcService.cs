using SS_Microservice.Common.Grpc.Product.Protos;
using SS_Microservice.Services.Basket.Application.Common.Interfaces;
using SS_Microservice.Services.Basket.Core;

namespace SS_Microservice.Services.Basket.Infrastructure.Services
{
    public class ProductGrpcService : IProductGrpcService
    {
        private readonly ProductProtoService.ProductProtoServiceClient _productProtoServiceClient;

        public ProductGrpcService(ProductProtoService.ProductProtoServiceClient productProtoServiceClient)
        {
            _productProtoServiceClient = productProtoServiceClient;
        }

        public async Task<ProductResponse> GetProductById(GetProductDetail request)
        {
            return await _productProtoServiceClient.GetProductInformationAsync(request);
        }
    }
}