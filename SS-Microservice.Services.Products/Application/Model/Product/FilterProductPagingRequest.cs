using SS_Microservice.Common.Model.Paging;

namespace SS_Microservice.Services.Products.Application.Model.Product
{
	public class FilterProductPagingRequest : PagingRequest
	{
		public decimal MinPrice { get; set; } = 0;
		public decimal MaxPrice { get; set; } = decimal.MaxValue;
		public List<long> BrandIds { get; set; } = new List<long>();
		public List<long> CategoryIds { get; set; } = new List<long>();
		public string CategorySlug { get; set; }
		public int? Rating { get; set; }
	}
}