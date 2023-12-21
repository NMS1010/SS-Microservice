namespace SS_Microservice.Services.Order.Application.Dtos
{
    public class StatisticTotalDto
    {
        public decimal Revenue { get; set; }
        public decimal Expense { get; set; }
        public int Users { get; set; }
        public int Orders { get; set; }
    }
}
