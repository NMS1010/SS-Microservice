namespace SS_Microservice.Services.UserOperation.Application.Models.Review
{
    public class GetOrderReviewRequest
    {
        public string UserId { get; set; }
        public List<long> OrderItemIds { get; set; }
    }
}
