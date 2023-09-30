using SS_Microservice.Common.Entities;

namespace SS_Microservice.Services.Order.Domain.Entities
{
    public class Order : BaseAuditableEntity<long>
    {
        public string UserId { get; set; }
        public int AddressId { get; set; }
        public string OtherCancelReason { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal Tax { get; set; }
        public decimal ShippingCost { get; set; }
        public bool PaymentStatus { get; set; }
        public string Note { get; set; }
        public long OrderStateId { get; set; }
        public OrderState OrderState { get; set; }
        public string Code { get; set; }
        public string DeliveryMethodType { get; set; }
        public Transaction Transaction { get; set; }
        public long OrderCancellationReasonId { get; set; }
        public OrderCancellationReason OrderCancellationReason { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}