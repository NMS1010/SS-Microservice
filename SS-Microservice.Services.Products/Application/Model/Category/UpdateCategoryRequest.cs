using System.Text.Json.Serialization;

namespace SS_Microservice.Services.Products.Application.Model.Category
{
    public class UpdateCategoryRequest
    {
        [JsonIgnore]
        public long Id { get; set; }
        public string Name { get; set; }
        public long? ParentId { get; set; }
        public IFormFile Image { get; set; }
        public string Slug { get; set; }
        public bool Status { get; set; }
    }
}