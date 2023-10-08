namespace SS_Microservice.Services.Products.Domain.Entities
{
    public class Category : BaseMongoEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string ParentId { get; set; }
        public string Slug { get; set; }
        public bool Status { get; set; } = true;
    }
}