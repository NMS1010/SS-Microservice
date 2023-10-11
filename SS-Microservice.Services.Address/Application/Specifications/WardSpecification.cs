using SS_Microservice.Common.Specifications;
using SS_Microservice.Services.Address.Application.Features.Ward.Queries;
using SS_Microservice.Services.Address.Domain.Entities;

namespace SS_Microservice.Services.Address.Application.Specifications
{
    public class WardSpecification : BaseSpecification<Ward>
    {
        public WardSpecification(GetWardByDistrictIdQuery query, bool isPaging = false)
        {
            string key = query.Search;
            if (query.DistrictId != -1)
            {
                if (!string.IsNullOrEmpty(key))
                {
                    Criteria = x => x.DistrictId == query.DistrictId && (
                           x.Name.ToLower().Contains(key)
                        || x.Code.ToLower().Contains(key)
                    );
                }
                else
                {
                    Criteria = x => x.DistrictId == query.DistrictId;
                }
            }
            if (!isPaging) return;
            int skip = (query.PageIndex - 1) * query.PageSize;
            int take = query.PageSize;
            ApplyPaging(take, skip);
        }
    }
}