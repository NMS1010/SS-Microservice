namespace SS_Microservice.Services.Order.Core.Entities
{
    public class OrderState
    {
        public int OrderStateId { get; set; }
        public string OrderStateName { get; set; }
        public int Order { get; set; }
        public string HexColor { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}