namespace SS_Microservice.Services.Order.Application.Models.Order
{
    public class UpdateOrderRequest
    {
        public long OrderId { get; set; }
        public int OrderStateId { get; set; }
    }
}