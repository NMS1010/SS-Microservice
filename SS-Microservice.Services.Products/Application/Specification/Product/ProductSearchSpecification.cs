using SS_Microservice.Common.Specifications;
using SS_Microservice.Services.Products.Application.Model.Product;

namespace SS_Microservice.Services.Products.Application.Specification.Product
{
	public class ProductSearchSpecification : BaseSpecification<Domain.Entities.Product>
	{
		public ProductSearchSpecification(SearchProductPagingRequest query, bool isPaging = false)
		{
			Criteria = x => x.Name.Contains(query.Search);

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