namespace SS_Microservice.Services.Order.Application.Models.OrderCancellationReason
{
    public class UpdateOrderCancellationReasonRequest
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public int Status { get; set; }
    }
}