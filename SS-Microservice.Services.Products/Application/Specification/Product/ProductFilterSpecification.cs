using SS_Microservice.Common.Specifications;
using SS_Microservice.Services.Products.Application.Common.Enums;
using SS_Microservice.Services.Products.Application.Model.Product;

namespace SS_Microservice.Services.Products.Application.Specification.Product
{
	public class ProductFilterSpecification : BaseSpecification<Domain.Entities.Product>
	{
		public ProductFilterSpecification(FilterProductPagingRequest query, bool isPaging = false)
		{
			Criteria = x =>
				   x.Variants.Min(m => m.PromotionalItemPrice ?? m.ItemPrice) >= query.MinPrice
				&& x.Variants.Max(mx => mx.PromotionalItemPrice ?? mx.ItemPrice) <= query.MaxPrice
				&& x.Status != PRODUCT_STATUS.INACTIVE
				&& (string.IsNullOrEmpty(query.Search) || x.Name.Contains(query.Search))
				&& (query.Rating == null || x.Rating >= query.Rating)
				&& (string.IsNullOrEmpty(query.CategorySlug) || x.Category.Slug == query.CategorySlug)
				&& (query.BrandIds.Count <= 0 || query.BrandIds.Contains(x.Brand.Id))
				&& (query.CategoryIds.Count <= 0 || query.CategoryIds.Contains(x.Category.Id));

			var columnName = query.ColumnName.ToLower();
			if (columnName == "price")
			{
				if (query.IsSortAscending)
					AddOrderBy(x => x.Variants.OrderBy(y => y.PromotionalItemPrice ?? y.ItemPrice)
						.Select(z => z.PromotionalItemPrice ?? z.ItemPrice)
						.FirstOrDefault());
				else
					AddOrderByDescending(x => x.Variants.OrderBy(y => y.PromotionalItemPrice ?? y.ItemPrice)
						.Select(z => z.PromotionalItemPrice ?? z.ItemPrice)
						.FirstOrDefault());
			}
			else
			{
				if (string.IsNullOrEmpty(query.ColumnName))
					query.ColumnName = "CreatedAt";
				AddSorting(query.ColumnName, query.IsSortAscending);
			}

			AddInclude(x => x.Images);
			AddInclude(x => x.Category);
			AddInclude(x => x.Sale);
			AddInclude(x => x.Brand);
			AddInclude(x => x.Unit);
			AddInclude(x => x.Variants);
			if (!isPaging) return;
			int skip = (query.PageIndex - 1) * query.PageSize;
			int take = query.PageSize;
			ApplyPaging(take, skip);
		}
	}
}