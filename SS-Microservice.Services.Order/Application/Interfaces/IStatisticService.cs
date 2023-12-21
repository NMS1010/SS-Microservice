using SS_Microservice.Services.Order.Application.Dtos;
using SS_Microservice.Services.Order.Application.Features.Statistic.Queries;

namespace SS_Microservice.Services.Order.Application.Interfaces
{
    public interface IStatisticService
    {
        Task<List<StatisticOrderStatusDto>> GetStatisticOrderStatus(GetStatisticOrderStatusQuery query);

        Task<List<StatisticTransactionDto>> GetTopLatestTransaction(GetStatisticTopLatestTransactionQuery query);

        Task<StatisticTotalDto> GetStatisticTotal(GetStatisticTotalQuery query);

        Task<List<StatisticRevenueDto>> GetStatisticRevenue(GetStatisticRevenueQuery query);

        Task<List<StatisticTopSellingProductYearDto>> GetStatisticTopSellingProductYear(
                       GetStatisticTopSellingProductYearQuery query);

        Task<List<StatisticTopSellingProductDto>> GetStatisticTopSellingProduct(
                       GetStatisticTopSellingProductQuery query);
    }
}
