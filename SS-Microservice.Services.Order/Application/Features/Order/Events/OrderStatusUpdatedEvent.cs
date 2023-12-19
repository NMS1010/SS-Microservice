using SS_Microservice.Contracts.Events.Order;

namespace SS_Microservice.Services.Order.Application.Features.Order.Events
{
    public class OrderStatusUpdatedEvent : IOrderStatusUpdatedEvent
    {
        public long OrderId { get; set; }
        public string OrderCode { get; set; }
        public string Image { get; set; }
        public string UserId { get; set; }
        public string Status { get; set; }
    }
}
