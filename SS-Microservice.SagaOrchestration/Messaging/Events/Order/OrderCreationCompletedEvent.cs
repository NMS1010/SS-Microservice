using SS_Microservice.Contracts.Events.Order;

namespace SS_Microservice.SagaOrchestration.Messaging.Events.Order
{
    public class OrderCreationCompletedEvent : IOrderCreationCompletedEvent
    {
        public Guid CorrelationId { get; set; }
        public string OrderCode { get; set; }
        public long OrderId { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Image { get; set; }
        public decimal TotalPrice { get; set; }
        public string PaymentMethod { get; set; }
        public string ReceiverEmail { get; set; }
        public string Receiver { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
