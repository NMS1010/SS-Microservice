using SS_Microservice.Common.Types.Entities;

namespace SS_Microservice.Services.Products.Domain.Entities
{
    public class Sale : BaseAuditableEntity<long>
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public string Image { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public double PromotionalPercent { get; set; }
		public string Slug { get; set; }
		public string Status { get; set; }
		public bool All { get; set; }
		public ICollection<Product> Products { get; set; }
	}
}