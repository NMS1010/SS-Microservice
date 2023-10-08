using SS_Microservice.Services.Products.Domain.Entities;

namespace SS_Microservice.Services.Products.Application.Dto
{
    public class ProductDto : BaseMongoEntity
    {
        public long SaleId { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public long Quantity { get; set; }
        public long Sold { get; set; }
        public string Unit { get; set; }
        public string Status { get; set; }
        public string Slug { get; set; }
        public int Rating { get; set; }
        public decimal Price { get; set; }
        public decimal PromotionalPrice { get; set; }
        public decimal Cost { get; set; }
        public string BrandId { get; set; }
        public BrandDto Brand { get; set; }
        public string CategoryId { get; set; }
        public CategoryDto Category { get; set; }
        public ICollection<ProductImageDto> Images { get; set; }
        public ICollection<VariantDto> Variants { get; set; }
    }
}