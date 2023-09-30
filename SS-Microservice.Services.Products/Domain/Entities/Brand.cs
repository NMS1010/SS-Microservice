namespace SS_Microservice.Services.Products.Domain.Entities
{
    public class Brand : BaseMongoEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}