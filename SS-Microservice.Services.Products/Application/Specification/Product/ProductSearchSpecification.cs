using SS_Microservice.Common.Specifications;
using SS_Microservice.Services.Products.Application.Common.Enums;
using SS_Microservice.Services.Products.Application.Features.Product.Queries;

namespace SS_Microservice.Services.Products.Application.Specification.Product
{
    public class ProductSearchSpecification : BaseSpecification<Domain.Entities.Product>
    {
        public ProductSearchSpecification(GetListSearchingProductQuery query, bool isPaging = false)
        {
            Criteria = x => x.Name.Contains(query.Search) && x.Status != PRODUCT_STATUS.INACTIVE;

            if (string.IsNullOrEmpty(query.ColumnName))
                query.ColumnName = "Name";
            AddSorting(query.ColumnName, query.IsSortAscending);

            if (!isPaging) return;
            int skip = (query.PageIndex - 1) * query.PageSize;
            int take = query.PageSize;
            ApplyPaging(take, skip);
        }
    }
}