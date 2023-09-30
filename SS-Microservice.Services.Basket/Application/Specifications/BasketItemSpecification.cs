using SS_Microservice.Common.Specifications;
using SS_Microservice.Services.Basket.Application.Features.Basket.Queries;
using SS_Microservice.Services.Basket.Domain.Entities;

namespace SS_Microservice.Services.Basket.Application.Specifications
{
    public class BasketItemSpecification : BaseSpecification<Domain.Entities.BasketItem>
    {
        public BasketItemSpecification(GetBasketQuery query, bool isPaging = false)
            : base(x => x.Basket.UserId == query.UserId)
        {
            AddInclude(x => x.Basket);

            if (!isPaging) return;
            int skip = (int)((query.PageIndex - 1) * query.PageSize);
            int take = (int)query.PageSize;
            ApplyPagging(take, skip);
        }

        public BasketItemSpecification(long basketId, string variantId)
            : base(x => x.BasketId == basketId && x.VariantId == variantId)
        {
        }

        public BasketItemSpecification(List<string> variantIds, long basketId)
            : base(x => x.BasketId == basketId && variantIds.Contains(x.VariantId))
        {
        }
    }
}