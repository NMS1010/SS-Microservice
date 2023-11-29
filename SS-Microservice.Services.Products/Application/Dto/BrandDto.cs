using SS_Microservice.Common.Entities;

namespace SS_Microservice.Services.Products.Application.Dto
{
	public class BrandDto : BaseAuditableEntity<long>
	{
		public string Name { get; set; }
		public string Code { get; set; }
		public string Description { get; set; }
		public string Image { get; set; }
		public bool Status { get; set; }
	}
}