using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SS_Microservice.Common.Specifications;
using SS_Microservice.Services.Order.Application.Features.OrderState.Queries;

namespace SS_Microservice.Services.Order.Application.Specifications
{
    public class OrderStateSpecification : BaseSpecification<Domain.Entities.OrderState>
    {
        public OrderStateSpecification(GetAllOrderStateQuery query, bool isPaging = false)
        {
            string key = query.Search;
            if (!string.IsNullOrEmpty(key))
            {
                Criteria = x => x.OrderStateName.ToLower().Contains(key)
                 || x.Id.ToString().Contains(key)
                 || x.Order.ToString().Contains(key)
                 || x.HexColor.ToLower().Contains(key);
            }
            if (!isPaging) return;
            int skip = (query.PageIndex - 1) * query.PageSize;
            int take = query.PageSize;
            ApplyPaging(take, skip);
        }
    }
}