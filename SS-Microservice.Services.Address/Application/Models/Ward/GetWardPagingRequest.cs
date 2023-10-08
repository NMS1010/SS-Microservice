using SS_Microservice.Common.Model.Paging;

namespace SS_Microservice.Services.Address.Application.Models.Ward
{
    public class GetWardPagingRequest : PagingRequest
    {
        public long DistrictId { get; set; }
    }
}