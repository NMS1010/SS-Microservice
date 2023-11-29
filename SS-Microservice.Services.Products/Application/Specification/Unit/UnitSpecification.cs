using SS_Microservice.Common.Specifications;
using SS_Microservice.Services.Products.Application.Features.Unit.Query;

namespace SS_Microservice.Services.Products.Application.Specification.Unit
{
    public class UnitSpecification : BaseSpecification<Domain.Entities.Unit>
    {
        public UnitSpecification(GetListUnitQuery query, bool isPaging = false)
        {
            var keyword = query.Search;

            if (!string.IsNullOrEmpty(keyword))
            {
                if (query.Status)
                    Criteria = x => x.Name.Contains(keyword) && x.Status == true;
                else
                    Criteria = x => x.Name.Contains(keyword);
            }
            else
            {
                if (query.Status)
                    Criteria = x => x.Status == true;
                else
                    Criteria = x => true;
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