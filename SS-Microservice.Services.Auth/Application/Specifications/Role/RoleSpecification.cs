using SS_Microservice.Common.Specifications;
using SS_Microservice.Services.Auth.Application.Features.Role.Queries;
using SS_Microservice.Services.Auth.Domain.Entities;

namespace SS_Microservice.Services.Auth.Application.Specifications.Role
{
    public class RoleSpecification : BaseSpecification<AppRole>
    {
        public RoleSpecification(string name) : base(x => x.NormalizedName.ToLower() == name)
        {
        }

        public RoleSpecification(GetListRoleQuery query, bool isPaging = false)
        {
            var keyword = query.Search;

            if (!string.IsNullOrEmpty(keyword))
            {
                Criteria = x => x.Name.Contains(keyword);
            }

            if (string.IsNullOrEmpty(query.ColumnName))
                query.ColumnName = "Id";
            AddSorting(query.ColumnName, query.IsSortAscending);

            if (!isPaging) return;
            int skip = (query.PageIndex - 1) * query.PageSize;
            int take = query.PageSize;
            ApplyPaging(take, skip);
        }
    }
}