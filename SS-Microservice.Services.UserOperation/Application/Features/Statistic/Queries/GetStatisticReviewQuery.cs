using MediatR;
using SS_Microservice.Services.UserOperation.Application.Dto;
using SS_Microservice.Services.UserOperation.Application.Interfaces;
using SS_Microservice.Services.UserOperation.Application.Models.Statistic;

namespace SS_Microservice.Services.UserOperation.Application.Features.Statistic.Queries
{
    public class GetStatisticReviewQuery : GetStatisticReviewRequest, IRequest<List<StatisticReviewDto>>
    {
    }

    public class GetStatisticReviewHandler : IRequestHandler<GetStatisticReviewQuery, List<StatisticReviewDto>>
    {
        private readonly IStatisticService _statisticService;

        public GetStatisticReviewHandler(IStatisticService statisticService)
        {
            _statisticService = statisticService;
        }

        public Task<List<StatisticReviewDto>> Handle(GetStatisticReviewQuery request, CancellationToken cancellationToken)
        {
            return _statisticService.GetStatisticReview(request);
        }
    }
}
