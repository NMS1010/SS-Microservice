using SS_Microservice.Common.Types.Model.Paging;
using System.Text.Json.Serialization;

namespace SS_Microservice.Services.UserOperation.Application.Models.UserFollowProduct
{
    public class GetFollowProductPagingRequest : PagingRequest
    {
        [JsonIgnore]
        public string UserId { get; set; }
    }
}
