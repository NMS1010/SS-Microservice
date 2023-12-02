using SS_Microservice.Services.UserOperation.Application.Features.UserFollowProduct.Commands;

namespace SS_Microservice.Services.UserOperation.Application.Interfaces
{
    public interface IUserFollowProductService
    {
        Task<bool> FollowProduct(FollowProductCommand command);

        Task<bool> UnFollowProduct(UnFollowProductCommand command);

        //Task<PaginatedResult<ProductDto>> GetListFollowProduct(GetFollowProductPagingRequest request);
    }
}
