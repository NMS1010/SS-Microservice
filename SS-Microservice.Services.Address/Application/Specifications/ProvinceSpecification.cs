using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SS_Microservice.Common.Specifications;
using SS_Microservice.Services.Address.Application.Features.Province.Queries;
using SS_Microservice.Services.Address.Domain.Entities;

namespace SS_Microservice.Services.Address.Application.Specifications
{
    public class ProvinceSpecification : BaseSpecification<Province>
    {
        public ProvinceSpecification(long id) : base(x => x.Id == id)
        {
            AddInclude(x => x.Districts);
        }
    }
}