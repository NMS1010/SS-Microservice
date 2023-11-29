using SS_Microservice.Common.Entities;

namespace SS_Microservice.Services.Products.Application.Dto
{
	public class CategoryDto : BaseAuditableEntity<long>
	{
		public string Name { get; set; }
		public string ParentName { get; set; }
		public string Image { get; set; }
		public string Slug { get; set; }
		public bool Status { get; set; }
	}
}