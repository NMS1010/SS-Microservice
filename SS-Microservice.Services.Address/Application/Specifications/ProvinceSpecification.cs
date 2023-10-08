using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SS_Microservice.Common.Specifications;
using SS_Microservice.Services.Address.Application.Features.Province.Queries;
using SS_Microservice.Services.Address.Domain.Entities;

namespace SS_Microservice.Services.Address.Application.Specifications
{
    public class ProvinceSpecification : BaseSpecification<Province>
    {
        public ProvinceSpecification(GetAllProvinceQuery query, bool isPaging = false)
        {
            string key = query.Keyword;
            if (!string.IsNullOrEmpty(key))
            {
                Criteria = x =>
                    x.Name.ToLower().Contains(key)
                 || x.Code.ToLower().Contains(key);
            }
            if (!isPaging) return;
            int skip = (int)((query.PageIndex - 1) * query.PageSize);
            int take = (int)query.PageSize;
            ApplyPagging(take, skip);
        }
    }
}