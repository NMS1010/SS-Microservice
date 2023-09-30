namespace SS_Microservice.Services.Products.Application.Model.Variant
{
    public class CreateVariantRequest
    {
        public string ProductId { get; set; }
        public string Name { get; set; }
        public decimal ItemPrice { get; set; }
        public decimal ItemCost { get; set; }
        public long Quantity { get; set; }
    }
}