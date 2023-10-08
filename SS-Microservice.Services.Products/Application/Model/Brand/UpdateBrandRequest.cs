namespace SS_Microservice.Services.Products.Application.Model.Brand
{
    public class UpdateBrandRequest
    {
        public string Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public bool Status { get; set; }
    }
}