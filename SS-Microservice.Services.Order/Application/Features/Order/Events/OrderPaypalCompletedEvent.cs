using SS_Microservice.Contracts.Events.Order;

namespace SS_Microservice.Services.Order.Application.Features.Order.Events
{
    public class OrderPaypalCompletedEvent : IOrderPaypalCompletedEvent
    {
        public long OrderId { get; set; }
        public string OrderCode { get; set; }
        public string Image { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public decimal TotalPrice { get; set; }
        public string PaymentMethod { get; set; }
        public string ReceiverEmail { get; set; }
        public string Receiver { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
