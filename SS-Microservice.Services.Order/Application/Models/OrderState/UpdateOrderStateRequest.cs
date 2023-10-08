namespace SS_Microservice.Services.Order.Application.Models.OrderState
{
    public class UpdateOrderStateRequest
    {
        public long OrderStateId { get; set; }
        public string OrderStateName { get; set; }
        public int Order { get; set; }
        public string HexColor { get; set; }
        public bool Status { get; set; }
    }
}