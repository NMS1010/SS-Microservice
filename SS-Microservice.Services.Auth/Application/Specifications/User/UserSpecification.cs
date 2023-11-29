using SS_Microservice.Common.Specifications;
using SS_Microservice.Services.Auth.Application.Features.User.Queries;
using SS_Microservice.Services.Auth.Domain.Entities;

namespace green_craze_be_v1.Application.Specification.User
{
    public class UserSpecification : BaseSpecification<AppUser>
    {
        public UserSpecification(GetListUserQuery query, bool isPaging = false)
        {
            var key = query.Search;
            if (!string.IsNullOrEmpty(key))
            {
                Criteria = x => x.Staff == null && x.Email.ToLower().Contains(key)
                || x.PhoneNumber.ToLower().Contains(key)
                || x.Gender.ToLower().Contains(key)
                || x.Dob.Value.ToString().Contains(key)
                || x.FirstName.ToLower().Contains(key)
                || x.LastName.ToLower().Contains(key);
            }
            else
            {
                Criteria = x => x.Staff == null;
            }
            AddInclude(x => x.Staff);

            if (string.IsNullOrEmpty(query.ColumnName))
                query.ColumnName = "Id";
            AddSorting(query.ColumnName, query.IsSortAscending);

            if (!isPaging) return;
            int skip = (query.PageIndex - 1) * query.PageSize;
            int take = query.PageSize;
            ApplyPaging(take, skip);
        }

        public UserSpecification(string userId) : base(x => x.Id == userId)
        {
            AddInclude(x => x.AppUserTokens);
        }

        public UserSpecification()
        {
            AddInclude(x => x.AppUserTokens);
        }

        public UserSpecification(string email, bool test = false) : base(x => x.Email == email)
        {
            AddInclude(x => x.AppUserTokens);
        }
    }
}