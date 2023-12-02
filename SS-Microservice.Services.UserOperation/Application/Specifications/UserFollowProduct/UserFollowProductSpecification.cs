using SS_Microservice.Common.Specifications;
using SS_Microservice.Services.UserOperation.Application.Models.UserFollowProduct;

namespace SS_Microservice.Services.UserOperation.Application.Specifications.UserFollowProduct
{
    public class UserFollowProductSpecification : BaseSpecification<Domain.Entities.UserFollowProduct>
    {
        public UserFollowProductSpecification(string userId, long productId)
            : base(x => x.UserId == userId && x.ProductId == productId)
        {
        }
        public UserFollowProductSpecification(GetFollowProductPagingRequest request, bool isPaging = false)
            : base(x => x.UserId == request.UserId)
        {
            if (!isPaging) return;
            int skip = (request.PageIndex - 1) * request.PageSize;
            int take = request.PageSize;
            ApplyPaging(take, skip);
        }
    }
}
