using SS_Microservice.Common.Repository;
using SS_Microservice.Services.UserOperation.Application.Dto;
using SS_Microservice.Services.UserOperation.Application.Features.Statistic.Queries;
using SS_Microservice.Services.UserOperation.Application.Interfaces;
using SS_Microservice.Services.UserOperation.Application.Specifications.Review;
using SS_Microservice.Services.UserOperation.Domain.Entities;

namespace SS_Microservice.Services.UserOperation.Application.Services
{
    public class StatisticService : IStatisticService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StatisticService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<StatisticReviewDto>> GetStatisticReview(GetStatisticReviewQuery query)
        {
            var resp = new List<StatisticReviewDto>();
            for (int i = 1; i <= 5; i++)
            {
                var count = await _unitOfWork.Repository<Review>()
                    .CountAsync(new ReviewSpecification(query.StartDate, query.EndDate, i));
                resp.Add(new StatisticReviewDto()
                {
                    Name = i + " Sao",
                    Value = count
                });
            }

            return resp;
        }
    }
}
