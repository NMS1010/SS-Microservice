using SS_Microservice.Services.Order.Application.Models.OrderItem;

namespace SS_Microservice.Services.Order.Application.Models.Order
{
    public class CreateOrderRequest
    {
        public int AddressId { get; set; }
        public long OrderStateId { get; set; }
        public long DeliveryId { get; set; }
        public string PaymentMethodId { get; set; }
        public string PaymentMethodType { get; set; }
        public string PaypalOrderId { get; set; }
        public string PaypalOrderStatus { get; set; }
        public string Note { get; set; }
        public string OtherCancelReason { get; set; }
        public long OrderCancellationReasonId { get; set; }
        public List<CreateOrderItemRequest> Items { get; set; }
    }
}