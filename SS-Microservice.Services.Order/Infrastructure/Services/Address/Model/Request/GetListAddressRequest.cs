using SS_Microservice.Common.Types.Model.Paging;

namespace SS_Microservice.Services.Order.Infrastructure.Services.Address.Model.Request
{
    public class GetListAddressRequest : PagingRequest
    {
        public string UserId { get; set; }
    }
}
