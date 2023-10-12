namespace SS_Microservice.Services.Order.Application.Models.OrderState
{
    public class UpdateOrderStateRequest
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int Order { get; set; }
        public string HexColor { get; set; }
        public bool Status { get; set; }
    }
}