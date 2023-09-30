namespace SS_Microservice.Services.Products.Application.Model.Product
{
    public class CreateProductImageRequest
    {
        public string ProductId { get; set; }
        public IFormFile Image { get; set; }

        public bool IsDefault { get; set; } = false;
    }
}