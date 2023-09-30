namespace SS_Microservice.Services.Order.Application.Models.Order
{
    public class UpdateOrderRequest
    {
        public long OrderId { get; set; }
        public long OrderStateId { get; set; }

        public string OtherCancelReason { get; set; }
        public long OrderCancellationReasonId { get; set; }
    }
}