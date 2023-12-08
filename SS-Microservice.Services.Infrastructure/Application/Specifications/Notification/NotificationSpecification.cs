using SS_Microservice.Common.Specifications;
using SS_Microservice.Services.Infrastructure.Application.Features.Notification.Queries;

namespace SS_Microservice.Services.Infrastructure.Application.Specifications.Notification
{
    public class NotificationSpecification : BaseSpecification<Domain.Entities.Notification>
    {
        public NotificationSpecification(GetListNotificationQuery query, bool isPaging = false)
        {
            var keyword = query.Search;
            if (string.IsNullOrEmpty(query.UserId))
                return;


            if (!string.IsNullOrEmpty(keyword))
            {
                Criteria = x => (x.Title.ToLower().Contains(keyword)
                || x.Content.Contains(keyword)) && x.UserId == query.UserId;
            }
            else
            {
                Criteria = x => x.UserId == query.UserId;
            }
            if (string.IsNullOrEmpty(query.ColumnName))
                query.ColumnName = "CreatedAt";
            AddSorting(query.ColumnName, query.IsSortAscending);

            if (!isPaging) return;
            int skip = (query.PageIndex - 1) * query.PageSize;
            int take = query.PageSize;
            ApplyPaging(take, skip);
        }
        public NotificationSpecification(string userId, long id) : base(x => x.Id == id && x.UserId == userId)
        {
        }

        public NotificationSpecification(string userId) : base(x => x.UserId == userId)
        {
        }

        public NotificationSpecification(string userId, bool status) : base(x => x.UserId == userId && x.Status == status)
        {
        }
    }
}