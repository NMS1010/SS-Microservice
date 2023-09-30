using SS_Microservice.Common.Specifications;
using SS_Microservice.Services.Order.Application.Features.Delivery.Queries;
using SS_Microservice.Services.Order.Application.Features.OrderState.Queries;

namespace SS_Microservice.Services.Order.Application.Specifications
{
    public class DeliverySpecification : BaseSpecification<Domain.Entities.Delivery>
    {
        public DeliverySpecification(GetAllDeliveryQuery query, bool isPaging = false)
        {
            string key = query.Keyword;
            if (!string.IsNullOrEmpty(key))
            {
                Criteria = x => x.Name.ToLower().Contains(key)
                    || x.Id.ToString().Contains(key)
                    || x.Description.Contains(key)
                    || x.Price.ToString().ToLower().Contains(key);
            }
            if (!isPaging) return;
            int skip = (int)((query.PageIndex - 1) * query.PageSize);
            int take = (int)query.PageSize;
            ApplyPagging(take, skip);
        }
    }
}