using SS_Microservice.Common.Types.Entities;

namespace SS_Microservice.Services.Products.Domain.Entities
{
    public class Unit : BaseAuditableEntity<long>
	{
		public string Name { get; set; }
		public bool Status { get; set; } = true;
		public ICollection<Product> Products { get; set; }
	}
}