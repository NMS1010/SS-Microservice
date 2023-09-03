using SS_Microservice.Common.Grpc.Product.Protos;

namespace SS_Microservice.Services.Order.Core.Interfaces
{
    public interface IProductGrpcService
    {
        Task<ProductResponse> GetProductById(string productId);
    }
}