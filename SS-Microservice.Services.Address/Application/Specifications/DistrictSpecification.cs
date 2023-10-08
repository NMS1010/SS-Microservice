using SS_Microservice.Common.Specifications;
using SS_Microservice.Services.Address.Application.Features.District.Queries;
using SS_Microservice.Services.Address.Domain.Entities;

namespace SS_Microservice.Services.Address.Application.Specifications
{
    public class DistrictSpecification : BaseSpecification<District>
    {
        public DistrictSpecification(GetDistrictByProvinceIdQuery query, bool isPaging = false)
        {
            string key = query.Keyword;
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
            int skip = (int)((query.PageIndex - 1) * query.PageSize);
            int take = (int)query.PageSize;
            ApplyPagging(take, skip);
        }
    }
}