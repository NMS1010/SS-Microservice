using SS_Microservice.Common.Specifications;
using SS_Microservice.Services.Products.Application.Model.Category;

namespace SS_Microservice.Services.Products.Application.Specification.Category
{
	public class CategorySpecification : BaseSpecification<Domain.Entities.Category>
	{
		public CategorySpecification(string slug) : base(x => x.Slug == slug)
		{
		}

		public CategorySpecification(GetCategoryPagingRequest query, bool isPaging = false)
		{
			var keyword = query.Search;

			if (!string.IsNullOrEmpty(keyword))
			{
				if (query.Status)
				{
					if (query.ParentCategoryId != null)
					{
						Criteria = x => (x.Name.Contains(keyword) || x.Slug.Contains(keyword)) && x.Status == true && x.ParentId == query.ParentCategoryId;
					}
					else
					{
						Criteria = x => (x.Name.Contains(keyword) || x.Slug.Contains(keyword)) && x.Status == true;
					}
				}
				else
				{
					if (query.ParentCategoryId != null)
					{
						Criteria = x => (x.Name.Contains(keyword) || x.Slug.Contains(keyword)) && x.ParentId == query.ParentCategoryId;
					}
					else
					{
						Criteria = x => x.Name.Contains(keyword) || x.Slug.Contains(keyword);
					}
				}
			}
			else
			{
				if (query.Status)
				{
					if (query.ParentCategoryId != null)
					{
						Criteria = x => x.Status == true && x.ParentId == query.ParentCategoryId;
					}
					else
					{
						Criteria = x => x.Status == true;
					}
				}
				else
				{
					if (query.ParentCategoryId != null)
					{
						Criteria = x => x.ParentId == query.ParentCategoryId;
					}
					else
					{
						Criteria = x => true;
					}
				}
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