using SS_Microservice.Common.Specifications;
using SS_Microservice.Services.Order.Application.Features.Transaction.Queries;

namespace SS_Microservice.Services.Order.Application.Specifications
{
    public class TransactionSpecification : BaseSpecification<Domain.Entities.Transaction>
    {
        public TransactionSpecification(GetAllTransactionQuery query, bool isPaging = false)
        {
            string key = query.Search;
            if (!string.IsNullOrEmpty(key))
            {
                Criteria = x => x.PaymentMethodType.ToLower().Contains(key)
                || x.Id.ToString().Contains(key)
                || x.TotalPay.ToString().Contains(key)
                || x.PaypalOrderStatus.Contains(key);
            }
            AddInclude(x => x.Order);
            if (!isPaging) return;
            int skip = (query.PageIndex - 1) * query.PageSize;
            int take = query.PageSize;
            ApplyPaging(take, skip);
        }

        public TransactionSpecification(long id)
            : base(x => x.Id == id)
        {
            AddInclude(x => x.Order);
        }
    }
}