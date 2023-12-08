namespace SS_Microservice.Services.Order.Application.Models.Order
{
    public class CreateOrderItemRequest
    {
        public int Quantity { get; set; }
        public long VariantId { get; set; }
    }
}