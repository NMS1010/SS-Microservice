using SS_Microservice.Common.Types.Model.Paging;

namespace SS_Microservice.Services.Products.Application.Model.Product
{
    public class GetProductPagingRequest : PagingRequest
	{
		public string CategorySlug { get; set; }
	}
}