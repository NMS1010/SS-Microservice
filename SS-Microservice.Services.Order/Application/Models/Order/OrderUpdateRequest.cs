namespace SS_Microservice.Services.Order.Application.Models.Order
{
    public class OrderUpdateRequest
    {
        public string OrderId { get; set; }
        public int OrderStateId { get; set; }
    }
}