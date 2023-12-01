using SS_Microservice.Common.Model.Paging;
using System.Text.Json.Serialization;

namespace SS_Microservice.Services.Basket.Application.Model
{
    public class GetBasketPagingRequest : PagingRequest
    {
        [JsonIgnore]
        public string UserId { get; set; }
    }
}