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
            string key = query.Keyword;
            if (!string.IsNullOrEmpty(key))
            {
                Criteria = x => x.Name.ToLower().Contains(key)
                 || x.Id.ToString().Contains(key)
                 || x.Note.Contains(key);
            }
            if (!isPaging) return;
            int skip = (int)((query.PageIndex - 1) * query.PageSize);
            int take = (int)query.PageSize;
            ApplyPagging(take, skip);
        }
    }
}