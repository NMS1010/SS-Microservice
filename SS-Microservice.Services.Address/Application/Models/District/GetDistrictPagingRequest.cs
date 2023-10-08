using SS_Microservice.Common.Model.Paging;

namespace SS_Microservice.Services.Address.Application.Models.District
{
    public class GetDistrictPagingRequest : PagingRequest
    {
        public long ProvinceId { get; set; } = -1;
    }
}