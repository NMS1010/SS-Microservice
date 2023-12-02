using SS_Microservice.Common.Specifications;
using SS_Microservice.Services.UserOperation.Application.Features.Review.Queries;

namespace SS_Microservice.Services.UserOperation.Application.Specifications.Review
{
    public class ReviewSpecification : BaseSpecification<Domain.Entities.Review>
    {
        public ReviewSpecification(int limit)
        {
            AddOrderByDescending(x => x.CreatedAt);
            ApplyPaging(limit, 0);
        }

        public ReviewSpecification(DateTime firstDate, DateTime lastDate, int rating)
            : base(x => x.CreatedAt >= firstDate && x.CreatedAt <= lastDate && x.Rating == rating)
        { }

        public ReviewSpecification(GetListReviewQuery query, bool isPaging = false)
        {
            var keyword = query.Search;
            if (!string.IsNullOrEmpty(keyword))
            {
                if (query.ProductId == null)
                {
                    Criteria = x => x.Title.Contains(keyword);
                }
                else
                {
                    Criteria = x => (x.Title.Contains(keyword)) && x.ProductId == query.ProductId;
                }
            }
            else
            {
                if (query.ProductId == null)
                {
                    Criteria = x => true;
                }
                else
                {
                    if (query.Status)
                    {
                        if (query.Rating != null)
                        {
                            Criteria = x => x.ProductId == query.ProductId && x.Status == true && x.Rating == query.Rating;
                        }
                        else
                        {
                            Criteria = x => x.ProductId == query.ProductId && x.Status == true;
                        }
                    }
                    else
                    {
                        Criteria = x => x.ProductId == query.ProductId;
                    }
                }
            }

            if (string.IsNullOrEmpty(query.ColumnName))
                query.ColumnName = "UpdatedAt";
            AddSorting(query.ColumnName, query.IsSortAscending);

            if (!isPaging) return;
            int skip = (query.PageIndex - 1) * query.PageSize;
            int take = query.PageSize;
            ApplyPaging(take, skip);
        }

        public ReviewSpecification(string userId, long id) : base(x => x.UserId == userId && x.Id == id)
        {
        }

        public ReviewSpecification(long orderItemId, string userId) : base(x => x.OrderItemId == orderItemId && x.UserId == userId)
        {
        }


        public ReviewSpecification(long productId, bool status = true) : base(x => x.ProductId == productId && x.Status == status)
        {
        }

        public ReviewSpecification(long id) : base(x => x.Id == id)
        {
        }
    }
}