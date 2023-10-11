using SS_Microservice.Common.Specifications;
using SS_Microservice.Services.Address.Application.Features.District.Queries;
using SS_Microservice.Services.Address.Domain.Entities;

namespace SS_Microservice.Services.Address.Application.Specifications
{
    public class DistrictSpecification : BaseSpecification<District>
    {
        public DistrictSpecification(GetDistrictByProvinceIdQuery query, bool isPaging = false)
        {
            string key = query.Search;
            if (query.ProvinceId != -1)
            {
                if (!string.IsNullOrEmpty(key))
                {
                    Criteria = x => x.ProvinceId == query.ProvinceId && (
                           x.Name.ToLower().Contains(key)
                        || x.Code.ToLower().Contains(key)
                    );
                }
                else
                {
                    Criteria = x => x.ProvinceId == query.ProvinceId;
                }
            }
            if (!isPaging) return;
            int skip = (query.PageIndex - 1) * query.PageSize;
            int take = query.PageSize;
            ApplyPaging(take, skip);
        }
    }
}