using SS_Microservice.Common.Types.Entities;

namespace SS_Microservice.Services.Products.Domain.Entities
{
    public class Variant : BaseAuditableEntity<long>
	{
		public string Name { get; set; }
		public string Sku { get; set; }
		public decimal ItemPrice { get; set; }
		public decimal? PromotionalItemPrice { get; set; }
		public decimal? TotalPromotionalPrice { get; set; }
		public string Status { get; set; }
		public long Quantity { get; set; }
		public decimal TotalPrice { get; set; }
		public Product Product { get; set; }
	}
}