using SS_Microservice.Common.Specifications;
using SS_Microservice.Services.Products.Application.Features.Brand.Queries;

namespace SS_Microservice.Services.Products.Application.Specification.Brand
{
    public class BrandSpecification : BaseSpecification<Domain.Entities.Brand>
    {
        public BrandSpecification(GetListBrandQuery query, bool isPaging = false)
        {
            var keyword = query.Search;

            if (!string.IsNullOrEmpty(keyword))
            {
                if (query.Status)
                    Criteria = x => (x.Name.Contains(keyword) || x.Code.Contains(keyword)) && x.Status == true;
                else
                    Criteria = x => x.Name.Contains(keyword) || x.Code.Contains(keyword);
            }
            else
            {
                if (query.Status)
                    Criteria = x => x.Status == true;
                else
                    Criteria = x => true;
            }

            if (string.IsNullOrEmpty(query.ColumnName))
                query.ColumnName = "CreatedAt";
            AddSorting(query.ColumnName, query.IsSortAscending);

            if (!isPaging) return;
            int skip = (query.PageIndex - 1) * query.PageSize;
            int take = query.PageSize;
            ApplyPaging(take, skip);
        }
    }
}