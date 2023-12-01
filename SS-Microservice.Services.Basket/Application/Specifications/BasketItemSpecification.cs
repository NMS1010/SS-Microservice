using SS_Microservice.Common.Specifications;
using SS_Microservice.Services.Basket.Application.Features.Basket.Queries;

namespace SS_Microservice.Services.Basket.Application.Specifications
{
    public class BasketItemSpecification : BaseSpecification<Domain.Entities.BasketItem>
    {
        public BasketItemSpecification(long cartId, long variantId)
            : base(x => x.VariantId == variantId && x.Basket.Id == cartId)
        {
            AddInclude(x => x.Basket);
        }

        public BasketItemSpecification(long cartItemId, string userId)
            : base(x => x.Id == cartItemId && x.Basket.UserId == userId)
        {
            AddInclude(x => x.Basket);
        }

        public BasketItemSpecification(GetListBasketByUserQuery query, bool isPaging = false)
            : base(x => x.Basket.UserId == query.UserId)
        {
            AddInclude(x => x.Basket);
            if (!isPaging) return;
            int skip = (query.PageIndex - 1) * query.PageSize;
            int take = query.PageSize;
            ApplyPaging(take, skip);
        }

        public BasketItemSpecification(List<long> variantIds, long basketId)
            : base(x => x.BasketId == basketId && variantIds.Contains(x.VariantId))
        {
            AddInclude(x => x.Basket);
        }
    }
}