using SS_Microservice.Common.Types.Entities;

namespace SS_Microservice.Services.Products.Application.Dto
{
    public class ProductImageDto : BaseAuditableEntity<long>
	{
		public string Image { get; set; }
		public long ProductId { get; set; }
		public double Size { get; set; }
		public string ContentType { get; set; }
		public bool IsDefault { get; set; }
	}
}