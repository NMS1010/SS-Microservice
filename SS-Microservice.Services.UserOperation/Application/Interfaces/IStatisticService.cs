using SS_Microservice.Services.UserOperation.Application.Dto;
using SS_Microservice.Services.UserOperation.Application.Features.Statistic.Queries;

namespace SS_Microservice.Services.UserOperation.Application.Interfaces
{
    public interface IStatisticService
    {
        Task<List<StatisticReviewDto>> GetStatisticReview(GetStatisticReviewQuery query);
    }
}
