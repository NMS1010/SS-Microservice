using SS_Microservice.Common.Types.Model.Paging;

namespace SS_Microservice.Services.Products.Application.Model.Category
{
    public class GetCategoryPagingRequest : PagingRequest
	{
		public long? ParentCategoryId { get; set; }
	}
}