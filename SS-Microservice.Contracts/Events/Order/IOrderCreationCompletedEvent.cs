using SS_Microservice.Common.Types.Messages;

namespace SS_Microservice.Contracts.Events.Order
{
    public interface IOrderCreationCompletedEvent : IEvent
    {
        string UserId { get; set; }
        string Email { get; set; }
        string UserName { get; set; }
        long OrderId { get; set; }
        string OrderCode { get; set; }
        string Image { get; set; }
        decimal TotalPrice { get; set; }
        string PaymentMethod { get; set; }
        public string ReceiverEmail { get; set; }
        public string Receiver { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

    }
}