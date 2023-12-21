namespace SS_Microservice.Services.Order.Application.Dtos
{
    public class StatisticTopSellingProductYearDto
    {
        public string Date { get; set; }
        public Dictionary<string, long> Products { get; set; }
    }
}
