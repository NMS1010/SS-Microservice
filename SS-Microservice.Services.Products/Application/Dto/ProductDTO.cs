using SS_Microservice.Services.Products.Core.Entities;

namespace SS_Microservice.Services.Products.Application.Dto
{
    public class ProductDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public string Origin { get; set; }
        public int Status { get; set; }
        public string MainImage { get; set; }
        public ICollection<ProductImageDTO> Images { get; set; }
    }
}