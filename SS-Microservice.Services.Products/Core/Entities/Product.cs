namespace SS_Microservice.Services.Products.Core.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public long Quantity { get; set; }
        public decimal Price { get; set; }
        public string Origin { get; set; }
        public int Status { get; set; }
        public string Slug { get; set; }
        public string MainImage { get; set; }
        public string CategoryId { get; set; }
        public ICollection<ProductImage> Images { get; set; } = new List<ProductImage>();
    }
}