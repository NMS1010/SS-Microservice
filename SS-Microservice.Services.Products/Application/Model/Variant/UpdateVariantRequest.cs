using System.Text.Json.Serialization;

namespace SS_Microservice.Services.Products.Application.Model.Variant
{
    public class UpdateVariantRequest
    {
        [JsonIgnore]
        public long Id { get; set; }
        public string Name { get; set; }
        public string Sku { get; set; }
        public int Quantity { get; set; }
        public decimal ItemPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; }
    }
}