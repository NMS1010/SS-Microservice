using SS_Microservice.Common.Types.Entities;

namespace SS_Microservice.Services.Products.Domain.Entities
{
    public class Category : BaseAuditableEntity<long>
	{
		public string Name { get; set; }
		public string Image { get; set; }
		public long? ParentId { get; set; }
		public string Slug { get; set; }
		public bool Status { get; set; } = true;
		public ICollection<Product> Products { get; set; }
	}
}