using SS_Microservice.Common.Specifications;
using SS_Microservice.Services.Order.Application.Features.OrderState.Queries;
using SS_Microservice.Services.Order.Application.Features.PaymentMethod.Queries;
using SS_Microservice.Services.Order.Domain.Entities;

namespace SS_Microservice.Services.Order.Application.Specifications
{
    public class PaymentMethodSpecification : BaseSpecification<PaymentMethod>
    {
        public PaymentMethodSpecification(GetAllPaymentMethodQuery query, bool isPaging = false)
        {
            string key = query.Search;
            if (!string.IsNullOrEmpty(key))
            {
                Criteria = x => x.Name.ToLower().Contains(key)
                || x.Id.ToString().Contains(key)
                || x.Code.Contains(key);
            }
            if (!isPaging) return;
            int skip = (query.PageIndex - 1) * query.PageSize;
            int take = query.PageSize;
            ApplyPaging(take, skip);
        }
    }
}