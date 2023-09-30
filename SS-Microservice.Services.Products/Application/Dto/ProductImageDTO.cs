using SS_Microservice.Services.Products.Domain.Entities;

namespace SS_Microservice.Services.Products.Application.Dto
{
    public class ProductImageDto : BaseMongoEntity
    {
        public string Image { get; set; }
        public long Size { get; set; }
        public string ContentType { get; set; }
        public bool IsDefault { get; set; }
    }
}