using SS_Microservice.Common.Specifications;
using SS_Microservice.Services.Address.Application.Features.District.Queries;
using SS_Microservice.Services.Address.Domain.Entities;

namespace SS_Microservice.Services.Address.Application.Specifications
{
    public class DistrictSpecification : BaseSpecification<District>
    {
        public DistrictSpecification(long id) : base(x => x.Id == id)
        {
            AddInclude(x => x.Province);
            AddInclude(x => x.Wards);
        }
    }
}