using MongoDB.Bson.Serialization.Attributes;
using SS_Microservice.Common.Entities;

namespace SS_Microservice.Services.Products.Domain.Entities
{
    public class Product : BaseMongoEntity
    {
        public string CategoryId { get; set; }
        public string BrandId { get; set; }
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
        public double Rating { get; set; }
        public decimal Cost { get; set; }
        public ICollection<ProductImage> Images { get; set; } = new List<ProductImage>();
        public ICollection<Variant> Variants { get; set; } = new List<Variant>();
    }
}