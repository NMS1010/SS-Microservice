using SS_Microservice.Common.Grpc.Product.Protos;
using System.Threading.Tasks;

namespace SS_Microservice.Services.Basket.Application.Interfaces
{
    public interface IProductGrpcService
    {
        Task<ProductResponse> GetProductByVariantId(GetProductDetailByVariant request);
    }
}