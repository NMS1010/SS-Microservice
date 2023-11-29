using SS_Microservice.Common.Specifications;
using SS_Microservice.Services.Auth.Application.Features.User.Queries;

namespace SS_Microservice.Services.Auth.Application.Specifications.User
{
    public class StaffSpecification : BaseSpecification<Domain.Entities.Staff>
    {
        public StaffSpecification(GetListStaffQuery query, bool isPaging = false)
        {
            AddInclude(x => x.User);
            var key = query.Search;
            if (!string.IsNullOrEmpty(key))
            {
                Criteria = x => x.User.Email.ToLower().Contains(key)
                || x.User.FirstName.ToLower().Contains(key)
                || x.User.LastName.ToLower().Contains(key)
                || x.Type.ToLower().Contains(key);
            }
            var columnName = query.ColumnName.ToLower();
            if (query.IsSortAscending)
            {
                if (columnName == nameof(Domain.Entities.AppUser.Email).ToLower())
                {
                    AddOrderBy(x => x.User.Email);
                }
                else if (columnName == nameof(Domain.Entities.AppUser.CreatedAt).ToLower())
                {
                    AddOrderBy(x => x.CreatedAt);
                }
                else if (columnName == nameof(Domain.Entities.AppUser.FirstName).ToLower())
                {
                    AddOrderBy(x => x.User.FirstName);
                }
                else if (columnName == nameof(Domain.Entities.AppUser.LastName).ToLower())
                {
                    AddOrderBy(x => x.User.LastName);
                }
                else if (columnName == nameof(Domain.Entities.AppUser.Status).ToLower())
                {
                    AddOrderBy(x => x.User.Status);
                }
                else if (columnName == nameof(Domain.Entities.Staff.Type).ToLower())
                {
                    AddOrderBy(x => x.Type);
                }
                else
                {
                    AddOrderBy(x => x.Id);
                }
            }
            else
            {
                if (columnName == nameof(Domain.Entities.AppUser.Email).ToLower())
                {
                    AddOrderByDescending(x => x.User.Email);
                }
                else if (columnName == nameof(Domain.Entities.AppUser.CreatedAt).ToLower())
                {
                    AddOrderByDescending(x => x.CreatedAt);
                }
                else if (columnName == nameof(Domain.Entities.AppUser.FirstName).ToLower())
                {
                    AddOrderByDescending(x => x.User.FirstName);
                }
                else if (columnName == nameof(Domain.Entities.AppUser.LastName).ToLower())
                {
                    AddOrderByDescending(x => x.User.LastName);
                }
                else if (columnName == nameof(Domain.Entities.AppUser.Status).ToLower())
                {
                    AddOrderByDescending(x => x.User.Status);
                }
                else if (columnName == nameof(Domain.Entities.Staff.Type).ToLower())
                {
                    AddOrderByDescending(x => x.Type);
                }
                else
                {
                    AddOrderByDescending(x => x.Id);
                }
            }
            if (!isPaging) return;
            int skip = (query.PageIndex - 1) * query.PageSize;
            int take = query.PageSize;
            ApplyPaging(take, skip);
        }

        public StaffSpecification(long staffId) : base(x => x.Id == staffId)
        {
            AddInclude(x => x.User);
        }
    }
}