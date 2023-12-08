using SS_Microservice.Common.Entities;

namespace SS_Microservice.Services.Order.Application.Dtos
{
    public class OrderDto : BaseAuditableEntity<long>
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
        public bool IsReview { get; set; }
        public DateTime? ReviewedDate { get; set; }
        //public UserDto User { get; set; }
        //public AddressDto Address { get; set; }
        public TransactionDto Transaction { get; set; }
        public OrderCancellationReasonDto CancelReason { get; set; }
        public List<OrderItemDto> Items { get; set; }
    }
}