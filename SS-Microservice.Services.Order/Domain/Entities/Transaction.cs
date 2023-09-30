using SS_Microservice.Common.Entities;

namespace SS_Microservice.Services.Order.Domain.Entities
{
    public class Transaction : BaseAuditableEntity<long>
    {
        public string PaymentMethodType { get; set; }
        public DateTime? PaidAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public decimal TotalPay { get; set; }
        public string PaypalOrderId { get; set; }
        public string PaypalOrderStatus { get; set; }
        public long OrderId { get; set; }
        public Order Order { get; set; }
    }
}