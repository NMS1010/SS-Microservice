namespace SS_Microservice.Services.Products.Application.Model.ProductImage
{
	public class CreateProductImageRequest
	{
		public IFormFile Image { get; set; }
		public long ProductId { get; set; }
	}
}