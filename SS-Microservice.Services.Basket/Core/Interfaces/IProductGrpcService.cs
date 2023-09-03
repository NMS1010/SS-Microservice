using SS_Microservice.Common.Grpc.Product.Protos;
using SS_Microservice.Services.Basket.Core;
using System.Threading.Tasks;

namespace SS_Microservice.Services.Basket.Core.Interfaces
{
    public interface IProductGrpcService
    {
        Task<ProductResponse> GetProductById(GetProductDetail request);
    }
}