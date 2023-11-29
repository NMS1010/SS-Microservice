namespace SS_Microservice.Services.Products.Application.Model.Variant
{
	public class CreateVariantRequest
	{
		public string Name { get; set; }
		public long? ProductId { get; set; }
		public string Sku { get; set; }
		public int Quantity { get; set; }
		public decimal ItemPrice { get; set; }
		public decimal TotalPrice { get; set; }
		public decimal? PromotionalItemPrice { get; set; }
	}
}