using SS_Microservice.Common.Types.Model.Paging;
using System.Text.Json.Serialization;

namespace SS_Microservice.Services.Address.Application.Models.Address
{
    public class GetAddressPagingRequest : PagingRequest
    {
        [JsonIgnore]
        public string UserId { get; set; }
    }
}