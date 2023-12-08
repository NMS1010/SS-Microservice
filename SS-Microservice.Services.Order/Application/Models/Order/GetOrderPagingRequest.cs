using SS_Microservice.Common.Model.Paging;
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