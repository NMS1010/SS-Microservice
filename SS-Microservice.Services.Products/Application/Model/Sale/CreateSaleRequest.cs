namespace SS_Microservice.Services.Products.Application.Model.Sale
{
	public class CreateSaleRequest
	{
		public string Name { get; set; }
		public string Slug { get; set; }
		public double PromotionalPercent { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public string Description { get; set; }
		public IFormFile Image { get; set; }
		public bool All { get; set; }
		public List<long> CategoryIds { get; set; }
	}
}