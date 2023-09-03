namespace SS_Microservice.Services.Order.Application.Models.OrderState
{
    public class UpdateOrderStateRequest
    {
        public int OrderStateId { get; set; }
        public string OrderStateName { get; set; }
        public int Order { get; set; }
        public string HexColor { get; set; }
    }
}