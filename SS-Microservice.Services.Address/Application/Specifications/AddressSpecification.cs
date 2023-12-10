using SS_Microservice.Common.Specifications;
using SS_Microservice.Services.Address.Application.Models.Address;

namespace SS_Microservice.Services.Address.Application.Specifications
{
    public class AddressSpecification : BaseSpecification<Domain.Entities.Address>
    {
        public AddressSpecification(string userId) : base(x => x.UserId == userId)
        {
            AddInclude(x => x.Province);
            AddInclude(x => x.District);
            AddInclude(x => x.Ward);
        }

        public AddressSpecification(GetAddressPagingRequest request, bool isPaging = false)
        {
            if (request.Status)
            {
                Criteria = x => x.Status == true && x.UserId == request.UserId;
            }
            else
            {
                Criteria = x => x.UserId == request.UserId;
            }
            AddInclude(x => x.Province);
            AddInclude(x => x.District);
            AddInclude(x => x.Ward);
            AddOrderByDescending(x => x.IsDefault);
            if (!isPaging) return;
            int skip = (request.PageIndex - 1) * request.PageSize;
            int take = request.PageSize;
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
            AddInclude(x => x.Province);
            AddInclude(x => x.District);
            AddInclude(x => x.Ward);
        }
    }
}