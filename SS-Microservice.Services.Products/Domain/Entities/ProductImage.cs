using SS_Microservice.Common.Entities;

namespace SS_Microservice.Services.Products.Domain.Entities
{
	public class ProductImage : BaseAuditableEntity<long>
	{
		public string Image { get; set; }
		public long Size { get; set; }
		public string ContentType { get; set; }
		public bool IsDefault { get; set; }
		public Product Product { get; set; }
	}
}