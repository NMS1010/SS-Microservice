namespace SS_Microservice.Services.Order.Infrastructure.Services.UserOperation.Model.Request
{
    public class GetOrderReviewRequest
    {
        public string UserId { get; set; }
        public List<long> OrderItemIds { get; set; }
    }
}
