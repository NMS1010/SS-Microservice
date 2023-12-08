using SS_Microservice.Common.Entities;

namespace SS_Microservice.Services.Order.Domain.Entities
{
    public class Order : BaseAuditableEntity<long>
    {
        public string OtherCancelReason { get; set; }
        public decimal TotalAmount { get; set; }
        public double Tax { get; set; }
        public decimal ShippingCost { get; set; }
        public bool PaymentStatus { get; set; }
        public string Note { get; set; }
        public string Status { get; set; }
        public string Code { get; set; }
        public string DeliveryMethod { get; set; }
        public string UserId { get; set; }
        public long AddressId { get; set; }
        public Transaction Transaction { get; set; }
        public OrderCancellationReason CancelReason { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}