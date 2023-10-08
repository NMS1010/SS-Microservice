using Microsoft.AspNetCore.Mvc;
using SS_Microservice.Services.Products.Application.Model.Variant;

namespace SS_Microservice.Services.Products.Application.Model.Product
{
    public class CreateProductRequest
    {
        public string CategoryId { get; set; }
        public string BrandId { get; set; }
        public long SaleId { get; set; } = -1;
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public long Quantity { get; set; }
        public string Unit { get; set; }
        public string Status { get; set; }
        public decimal Cost { get; set; }
        public IFormFile Image { get; set; }

        public List<IFormFile> SubImages { get; set; }
        //public List<CreateVariantRequest> Variants { get; set; }
    }
}