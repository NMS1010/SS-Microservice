namespace SS_Microservice.Services.Products.Application.Model.Product
{
    public class ProductImageUpdateRequest
    {
        public string ProductId { get; set; }
        public string ProductImageId { get; set; }
        public IFormFile Image { get; set; }
    }
}