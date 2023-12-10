using SS_Microservice.Common.Types.Model.Paging;
using SS_Microservice.Services.UserOperation.Application.Features.UserFollowProduct.Commands;
using SS_Microservice.Services.UserOperation.Application.Models.UserFollowProduct;
using SS_Microservice.Services.UserOperation.Infrastructure.Services.Product.Model.Response;

namespace SS_Microservice.Services.UserOperation.Application.Interfaces
{
    public interface IUserFollowProductService
    {
        Task<bool> FollowProduct(FollowProductCommand command);

        Task<bool> UnFollowProduct(UnFollowProductCommand command);

        Task<PaginatedResult<ProductDto>> GetListFollowProduct(GetFollowProductPagingRequest request);
    }
}
