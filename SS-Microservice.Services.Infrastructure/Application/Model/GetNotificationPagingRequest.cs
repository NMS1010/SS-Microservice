using SS_Microservice.Common.Model.Paging;

namespace SS_Microservice.Services.Infrastructure.Application.Model
{
    public class GetNotificationPagingRequest : PagingRequest
    {
        public int Status { get; set; } = -1;
    }
}