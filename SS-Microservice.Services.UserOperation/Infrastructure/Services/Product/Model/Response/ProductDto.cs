namespace SS_Microservice.Services.UserOperation.Infrastructure.Services.Product.Model.Response
{
    public class ProductDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public double Rating { get; set; }
        public string Code { get; set; }
        public string Slug { get; set; }
        public CategoryDto Category { get; set; }
        public List<ProductImageDto> Images { get; set; }
        public List<VariantDto> Variants { get; set; }
    }

    public class CategoryDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
    public class ProductImageDto
    {
        public long Id { get; set; }
        public string Image { get; set; }
        public bool IsDefault { get; set; }
    }
    public class VariantDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
