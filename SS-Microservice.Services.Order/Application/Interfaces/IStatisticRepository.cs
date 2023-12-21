using SS_Microservice.Services.Order.Application.Dtos;
using SS_Microservice.Services.Order.Application.Features.Statistic.Queries;

namespace SS_Microservice.Services.Order.Application.Interfaces
{
    public interface IStatisticRepository
    {
        Task<List<StatisticTopSellingProductDto>> GetStatisticTopSellingProduct(
            GetStatisticTopSellingProductQuery request);
    }
}
