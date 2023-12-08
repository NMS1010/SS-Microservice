using SS_Microservice.Common.Grpc.Product.Protos;

namespace SS_Microservice.Services.Basket.Application.Interfaces
{
    public interface IProductGrpcService
    {
        Task<ProductCustomGrpcResponse> GetProductByVariant(GetProductByVariant request);
    }
}