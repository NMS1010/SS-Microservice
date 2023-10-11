using SS_Microservice.Common.Specifications;
using SS_Microservice.Services.Address.Application.Features.Address.Queries;

namespace SS_Microservice.Services.Address.Application.Specifications
{
    public class AddressSpecification : BaseSpecification<Domain.Entities.Address>
    {
        public AddressSpecification(string userId, long addressId) : base(x => x.UserId == userId && x.Id == addressId)
        {
            AddInclude(x => x.Ward);
            AddInclude(x => x.District);
            AddInclude(x => x.Province);
        }

        public AddressSpecification(GetAllAddressQuery query, bool isPaging = false)
        {
            string key = query.Search;

            if (!string.IsNullOrEmpty(query.UserId))
            {
                if (!string.IsNullOrEmpty(key))
                {
                    Criteria = x => x.UserId == query.UserId && (
                           x.Receiver.ToLower().Contains(key)
                        || x.Email.ToLower().Contains(key)
                        || x.Phone.ToLower().Contains(key)
                        || x.Street.ToLower().Contains(key)
                        || x.Ward.Name.ToLower().Contains(key)
                        || x.District.Name.ToLower().Contains(key)
                        || x.Province.Name.ToLower().Contains(key)
                    );
                }
                else
                {
                    Criteria = x => x.UserId == query.UserId;
                }
            }
            AddInclude(x => x.Ward);
            AddInclude(x => x.District);
            AddInclude(x => x.Province);
            if (!isPaging) return;
            int skip = (int)((query.PageIndex - 1) * query.PageSize);
            int take = (int)query.PageSize;
            ApplyPaging(take, skip);
        }
    }
}