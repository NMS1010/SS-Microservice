using SS_Microservice.Common.Types.Model.Paging;
using System.Text.Json.Serialization;

namespace SS_Microservice.Services.Basket.Application.Model
{
    public class GetBasketPagingRequest : PagingRequest
    {
        [JsonIgnore]
        public string UserId { get; set; }
    }
}