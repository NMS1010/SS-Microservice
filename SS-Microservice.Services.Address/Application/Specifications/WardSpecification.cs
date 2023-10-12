using SS_Microservice.Common.Specifications;
using SS_Microservice.Services.Address.Application.Features.Ward.Queries;
using SS_Microservice.Services.Address.Domain.Entities;

namespace SS_Microservice.Services.Address.Application.Specifications
{
    public class WardSpecification : BaseSpecification<Ward>
    {
        public WardSpecification(long id) : base(x => x.Id == id)
        {
            AddInclude(x => x.District);
        }
    }
}