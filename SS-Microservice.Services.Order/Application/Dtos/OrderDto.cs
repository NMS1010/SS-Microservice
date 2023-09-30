using SS_Microservice.Common.Entities;
using SS_Microservice.Services.Order.Domain.Entities;

namespace SS_Microservice.Services.Order.Application.Dtos
{
    public class OrderDto : BaseAuditableEntity<long>
    {
        public int AddressId { get; set; }
        public string OtherCancelReason { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal Tax { get; set; }
        public decimal ShippingCost { get; set; }
        public bool PaymentStatus { get; set; }
        public string Note { get; set; }

        public string Code { get; set; }
        public string DeliveryMethodType { get; set; }

        public OrderCancellationReasonDto OrderCancellationReason { get; set; }
        public OrderStateDto OrderState { get; set; }

        public TransactionDto Transaction { get; set; }

        public List<OrderItemDto> OrderItems { get; set; }
    }
}