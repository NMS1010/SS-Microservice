using SS_Microservice.Services.Products.Domain.Entities;

namespace SS_Microservice.Services.Products.Application.Dto
{
    public class BrandDto : BaseMongoEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public bool Status { get; set; }
        public string Image { get; set; }
    }
}