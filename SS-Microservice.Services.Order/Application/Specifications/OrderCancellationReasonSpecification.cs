using SS_Microservice.Common.Specifications;
using SS_Microservice.Services.Order.Application.Features.OrderCancellationReason.Queries;
using SS_Microservice.Services.Order.Application.Features.OrderState.Queries;
using SS_Microservice.Services.Order.Domain.Entities;

namespace SS_Microservice.Services.Order.Application.Specifications
{
    public class OrderCancellationReasonSpecification : BaseSpecification<OrderCancellationReason>
    {
        public OrderCancellationReasonSpecification(GetAllOrderCancellationReasonQuery query, bool isPaging = false)
        {
            string key = query.Search;
            if (!string.IsNullOrEmpty(key))
            {
                Criteria = x => x.Name.ToLower().Contains(key)
                 || x.Id.ToString().Contains(key)
                 || x.Note.Contains(key);
            }
            if (!isPaging) return;
            int skip = (query.PageIndex - 1) * query.PageSize;
            int take = query.PageSize;
            ApplyPaging(take, skip);
        }
    }
}