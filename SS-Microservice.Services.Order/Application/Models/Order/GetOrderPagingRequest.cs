using SS_Microservice.Common.Types.Model.Paging;
using System.Text.Json.Serialization;

namespace SS_Microservice.Services.Order.Application.Models.Order
{
    public class GetOrderPagingRequest : PagingRequest
    {
        [JsonIgnore]
        public string UserId { get; set; }

        public string OrderStatus { get; set; }
    }
}