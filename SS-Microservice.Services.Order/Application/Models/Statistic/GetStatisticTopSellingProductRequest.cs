namespace SS_Microservice.Services.Order.Application.Models.Statistic
{
    public class GetStatisticTopSellingProductRequest
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int Top { get; set; } = 5;
    }
}
