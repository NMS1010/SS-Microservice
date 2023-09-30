using SS_Microservice.Services.Products.Domain.Entities;

namespace SS_Microservice.Services.Products.Application.Dto
{
    public class VariantDto : BaseMongoEntity
    {
        public string Name { get; set; }
        public string Sku { get; set; }
        public decimal ItemPrice { get; set; }
        public string Status { get; set; }
        public long Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    }
}