using SS_Microservice.Common.Specifications;
using SS_Microservice.Services.Address.Application.Features.Address.Queries;

namespace SS_Microservice.Services.Address.Application.Specifications
{
    public class AddressSpecification : BaseSpecification<Domain.Entities.Address>
    {
        public AddressSpecification(string userId) : base(x => x.UserId == userId)
        {
        }

        public AddressSpecification(GetListAddressQuery query, bool isPaging = false)
            : base(x => x.UserId == query.UserId)
        {
            AddInclude(x => x.Province);
            AddInclude(x => x.District);
            AddInclude(x => x.Ward);
            AddOrderByDescending(x => x.IsDefault);
            if (!isPaging) return;
            int skip = (query.PageIndex - 1) * query.PageSize;
            int take = query.PageSize;
            ApplyPaging(take, skip);
        }

        public AddressSpecification(string userId, long id)
            : base(x => x.UserId == userId && x.Id == id)
        {
            AddInclude(x => x.Province);
            AddInclude(x => x.District);
            AddInclude(x => x.Ward);
            AddOrderByDescending(x => x.IsDefault);
        }

        public AddressSpecification(string userId, bool isDefault)
            : base(x => x.UserId == userId && x.IsDefault == isDefault)
        {
        }
    }
}