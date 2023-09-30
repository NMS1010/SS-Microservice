namespace SS_Microservice.Services.Products.Application.Model.Variant
{
    public class UpdateVariantRequest
    {
        public string ProductId { get; set; }
        public string VariantId { get; set; }
        public string Name { get; set; }
        public decimal ItemPrice { get; set; }
        public decimal ItemCost { get; set; }
        public long Quantity { get; set; }
        public string Status { get; set; }
    }
}