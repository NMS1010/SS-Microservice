namespace SS_Microservice.Common.Messages.Models
{
    public class ProductStock
    {
        public long ProductId { get; set; }
        public long VariantId { get; set; }
        public long Quantity { get; set; }
    }
}