using SS_Microservice.Common.Model.Paging;
using System.Text.Json.Serialization;

namespace SS_Microservice.Services.Infrastructure.Application.Model.Notification
{
    public class GetNotificationPagingRequest : PagingRequest
    {
        [JsonIgnore]
        public string UserId { get; set; }
    }
}