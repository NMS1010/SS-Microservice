namespace SS_Microservice.Services.Products.Application.Model.Category
{
    public class UpdateCategoryRequest
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
        public string ParentId { get; set; }
        public bool Status { get; set; }
    }
}