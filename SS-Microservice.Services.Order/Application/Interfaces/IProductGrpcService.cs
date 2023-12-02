using SS_Microservice.Common.Grpc.Product.Protos;

namespace SS_Microservice.Services.Order.Application.Interfaces
{
    public interface IProductGrpcService
    {
        Task<ProductCustomGrpcResponse> GetProductByVariantId(long variantId);
    }
}