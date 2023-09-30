namespace SS_Microservice.Services.Products.Application.Model.Product
{
    public class UpdateProductImageRequest
    {
        public string ProductId { get; set; }
        public string ProductImageId { get; set; }
        public IFormFile Image { get; set; }
        public bool IsDefault { get; set; }
    }
}