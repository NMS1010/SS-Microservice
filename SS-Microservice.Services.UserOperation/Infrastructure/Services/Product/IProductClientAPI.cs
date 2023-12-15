using RestEase;
using SS_Microservice.Common.Types.Model.CustomResponse;
using SS_Microservice.Common.Types.Model.Paging;
using SS_Microservice.Services.UserOperation.Infrastructure.Services.Product.Model.Request;
using SS_Microservice.Services.UserOperation.Infrastructure.Services.Product.Model.Response;

namespace SS_Microservice.Services.UserOperation.Infrastructure.Services.Product
{
    public interface IProductClientAPI
    {
        [Get("api/products")]
        Task<CustomAPIResponse<PaginatedResult<ProductDto>>> GetListProduct([Query] GetProductPagingRequest request);

        [Get("api/products/internal/{id}")]
        Task<CustomAPIResponse<ProductDto>> GetProduct([Path] long id);

    }
}
