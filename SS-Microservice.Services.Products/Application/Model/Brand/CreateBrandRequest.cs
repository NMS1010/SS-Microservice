namespace SS_Microservice.Services.Products.Application.Model.Brand
{
    public class CreateBrandRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }

        public IFormFile Image { get; set; }
    }
}