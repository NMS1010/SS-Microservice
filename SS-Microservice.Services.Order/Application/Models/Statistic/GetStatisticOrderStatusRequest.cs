namespace SS_Microservice.Services.Order.Application.Models.Statistic
{
    public class GetStatisticOrderStatusRequest
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
