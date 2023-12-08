using SS_Microservice.Common.Specifications;
using SS_Microservice.Services.Order.Application.Models.Transaction;

namespace SS_Microservice.Services.Order.Application.Specifications.Transaction
{
    public class TransactionSpecification : BaseSpecification<Domain.Entities.Transaction>
    {
        public TransactionSpecification()
        {
            AddInclude(x => x.Order);
        }

        public TransactionSpecification(int limit)
        {
            AddInclude(x => x.Order);
            AddOrderByDescending(x => x.CreatedAt);
            ApplyPaging(limit, 0);
        }

        public TransactionSpecification(DateTime firstDate, DateTime lastDate)
            : base(x => x.CreatedAt >= firstDate && x.CreatedAt <= lastDate)
        {
            AddInclude(x => x.Order);
        }

        public TransactionSpecification(GetTransactionPagingRequest request, bool isPaging = false)
        {
            var keyword = request.Search;
            AddInclude(x => x.Order);
            if (!string.IsNullOrEmpty(keyword))
            {
                Criteria = x => x.PaymentMethod.ToLower().Contains(keyword)
                || x.Order.Code.ToLower().Contains(keyword);
            }

            var columnName = request.ColumnName.ToLower();
            if (columnName == nameof(Domain.Entities.Transaction.Order.Code).ToLower())
            {
                if (request.IsSortAscending)
                    AddOrderBy(x => x.Order.Code);
                else
                    AddOrderByDescending(x => x.Order.Code);
            }
            else
            {
                if (string.IsNullOrEmpty(request.ColumnName))
                    request.ColumnName = "Id";
                AddSorting(request.ColumnName, request.IsSortAscending);
            }

            if (!isPaging) return;
            int skip = (request.PageIndex - 1) * request.PageSize;
            int take = request.PageSize;
            ApplyPaging(take, skip);
        }

        public TransactionSpecification(long id) : base(x => x.Id == id)
        {
            AddInclude(x => x.Order);
        }
    }
}