using MediatR;
using SS_Microservice.Services.Order.Application.Dtos;
using SS_Microservice.Services.Order.Application.Interfaces;
using SS_Microservice.Services.Order.Application.Models.Statistic;

namespace SS_Microservice.Services.Order.Application.Features.Statistic.Queries
{
    public class GetStatisticOrderStatusQuery : GetStatisticOrderStatusRequest, IRequest<List<StatisticOrderStatusDto>>
    {
    }

    public class GetStatisticOrderStatusHandler : IRequestHandler<GetStatisticOrderStatusQuery, List<StatisticOrderStatusDto>>
    {
        private readonly IStatisticService _statisticService;

        public GetStatisticOrderStatusHandler(IStatisticService statisticService)
        {
            _statisticService = statisticService;
        }

        public async Task<List<StatisticOrderStatusDto>> Handle(GetStatisticOrderStatusQuery request, CancellationToken cancellationToken)
        {
            return await _statisticService.GetStatisticOrderStatus(request);
        }
    }
}
