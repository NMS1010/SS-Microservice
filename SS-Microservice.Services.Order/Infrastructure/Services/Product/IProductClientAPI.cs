using RestEase;
using SS_Microservice.Common.Types.Model.CustomResponse;
using SS_Microservice.Common.Types.Model.Paging;
using SS_Microservice.Services.Order.Infrastructure.Services.Product.Model.Request;
using SS_Microservice.Services.Order.Infrastructure.Services.Product.Model.Response;

namespace SS_Microservice.Services.Order.Infrastructure.Services.Product
{
    [SerializationMethods(Query = QuerySerializationMethod.Serialized)]
    public interface IProductClientAPI
    {
        [Get("api/products")]
        Task<CustomAPIResponse<PaginatedResult<ProductDto>>> GetListProduct([Query] GetProductPagingRequest request);
    }
}
