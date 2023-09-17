namespace SS_Microservice.Services.Products.Core.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string ParentId { get; set; }
        public string Slug { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}