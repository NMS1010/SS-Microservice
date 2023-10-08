using SS_Microservice.Services.Products.Domain.Entities;

namespace SS_Microservice.Services.Products.Application.Dto
{
    public class CategoryDto : BaseMongoEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Slug { get; set; }
        public string ParentId { get; set; }
        public bool Status { get; set; }
    }
}