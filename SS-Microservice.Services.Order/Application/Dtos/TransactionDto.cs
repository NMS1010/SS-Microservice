using SS_Microservice.Common.Entities;

namespace SS_Microservice.Services.Order.Application.Dtos
{
    public class TransactionDto : BaseAuditableEntity<long>
    {
        public string PaymentMethod { get; set; }
        public DateTime? PaidAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public decimal TotalPay { get; set; }
        public string PaypalOrderId { get; set; }
        public string PaypalOrderStatus { get; set; }

        public long OrderId { get; set; }
    }
}