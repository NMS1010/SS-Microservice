using System.Text.Json.Serialization;

namespace SS_Microservice.Services.Products.Application.Model.ProductImage
{
    public class UpdateProductImageRequest
    {
        [JsonIgnore]
        public long Id { get; set; }
        public IFormFile Image { get; set; }
    }
}