using SS_Microservice.Common.Specifications;
using SS_Microservice.Services.Infrastructure.Application.Features.Notification.Queries;

namespace SS_Microservice.Services.Infrastructure.Application.Specifications
{
    public class NotifcationSpecification : BaseSpecification<Domain.Entities.Notification>
    {
        public NotifcationSpecification(GetAllNotificationQuery query, bool isPaging = false)
        {
            var key = query.Keyword;
            if (!string.IsNullOrEmpty(query.UserId))
            {
                if (query.Status == -1)
                {
                    Criteria = x => x.UserId == query.UserId;
                }
                else
                {
                    Criteria = x => x.UserId == query.UserId
                        && x.Status == Convert.ToBoolean(query.Status);
                }
            }
            AddOrderByDecending(x => x.CreatedAt);
            if (!isPaging) return;
            int skip = (int)((query.PageIndex - 1) * query.PageSize);
            int take = (int)query.PageSize;
            ApplyPagging(take, skip);
        }
    }
}