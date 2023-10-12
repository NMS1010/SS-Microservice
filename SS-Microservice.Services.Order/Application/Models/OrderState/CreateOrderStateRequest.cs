namespace SS_Microservice.Services.Order.Application.Models.OrderState
{
    public class CreateOrderStateRequest
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public int Order { get; set; }
        public string HexColor { get; set; }
    }
}