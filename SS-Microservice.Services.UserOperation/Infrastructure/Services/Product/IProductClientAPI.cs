using RestEase;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Services.Auth.Application.Model.CustomResponse;
using SS_Microservice.Services.UserOperation.Infrastructure.Services.Product.Model.Request;
using SS_Microservice.Services.UserOperation.Infrastructure.Services.Product.Model.Response;

namespace SS_Microservice.Services.UserOperation.Infrastructure.Services.Product
{
    public interface IProductClientAPI
    {
        [Get("api/products")]
        Task<CustomAPIResponse<PaginatedResult<ProductDto>>> GetListProduct([Query] GetProductPagingRequest request);

        [Get("api/products/{id}")]
        Task<CustomAPIResponse<ProductDto>> GetProduct([Path] long id);

    }
}
