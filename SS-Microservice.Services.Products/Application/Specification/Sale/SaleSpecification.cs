using SS_Microservice.Common.Specifications;
using SS_Microservice.Services.Products.Application.Model.Sale;

namespace SS_Microservice.Services.Products.Application.Specification.Sale
{
	public class SaleSpecification : BaseSpecification<Domain.Entities.Sale>
	{
		public SaleSpecification(long id) : base(x => x.Id == id)
		{
			AddInclude(x => x.Products);
		}

		public SaleSpecification(bool latest)
		{
			AddOrderByDescending(x => x.StartDate);
			ApplyPaging(1, 0);
			AddInclude(x => x.Products);
		}

		public SaleSpecification(GetSalePagingRequest query, bool isPaging = false)
		{
			var keyword = query.Search;

			if (!string.IsNullOrEmpty(keyword))
			{
				Criteria = x => x.Name.Contains(keyword) || x.PromotionalPercent.ToString().Contains(keyword);
			}

			if (string.IsNullOrEmpty(query.ColumnName))
				query.ColumnName = "Id";
			AddSorting(query.ColumnName, query.IsSortAscending);

			if (!isPaging) return;
			AddInclude(x => x.Products);
			int skip = (query.PageIndex - 1) * query.PageSize;
			int take = query.PageSize;
			ApplyPaging(take, skip);
		}
	}
}