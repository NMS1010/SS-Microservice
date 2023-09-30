using SS_Microservice.Common.Specifications;
using SS_Microservice.Services.Order.Application.Features.Order.Queries;
using SS_Microservice.Services.Order.Domain.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SS_Microservice.Services.Order.Application.Specifications
{
    public class OrderSpecification : BaseSpecification<Domain.Entities.Order>
    {
        public OrderSpecification(GetAllOrderQuery query, bool isPaging = true)
        {
            var userId = query.UserId;
            var key = query.Keyword;

            if (!string.IsNullOrEmpty(query.UserId))
            {
                if (!string.IsNullOrEmpty(key))
                {
                    Criteria = x => x.UserId == userId && (
                        x.OrderItems
                            .Any(oi => oi.ProductName.ToLower().Contains(key))
                        || x.Id.ToString().Contains(key));
                }
                else
                {
                    Criteria = x => x.UserId == userId;
                }
            }
            else if (!string.IsNullOrEmpty(key))
            {
                Criteria = x => x.OrderItems
                        .Any(oi => oi.ProductName.ToLower().Contains(key))
                    || x.Id.ToString().Contains(key);
            }
            AddInclude(x => x.OrderState);
            AddInclude(x => x.OrderItems);

            if (!isPaging) return;
            int skip = (int)((query.PageIndex - 1) * query.PageSize);
            int take = (int)query.PageSize;
            ApplyPagging(take, skip);
        }

        public OrderSpecification(long orderId, string userId)
            : base(x => x.Id == orderId
                && x.UserId == userId)
        {
            AddInclude(x => x.OrderState);
            AddInclude(x => x.OrderItems);
            AddInclude(x => x.Transaction);
        }
    }
}