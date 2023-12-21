using MediatR;
using SS_Microservice.Common.Exceptions;
using SS_Microservice.Services.Order.Application.Dtos;
using SS_Microservice.Services.Order.Application.Interfaces;
using SS_Microservice.Services.Order.Infrastructure.Services.Auth;

namespace SS_Microservice.Services.Order.Application.Features.Statistic.Queries
{
    public class GetStatisticTopLatestTransactionQuery : IRequest<List<StatisticTransactionDto>>
    {
        public int Top { get; set; } = 5;
    }

    public class GetStatisticTopLatestTransactionHandler : IRequestHandler<GetStatisticTopLatestTransactionQuery, List<StatisticTransactionDto>>
    {
        private readonly IStatisticService _statisticService;
        private readonly IAuthClientAPI _authClientAPI;

        public GetStatisticTopLatestTransactionHandler(IStatisticService statisticService, IAuthClientAPI authClientAPI)
        {
            _statisticService = statisticService;
            _authClientAPI = authClientAPI;
        }

        public async Task<List<StatisticTransactionDto>> Handle(GetStatisticTopLatestTransactionQuery request, CancellationToken cancellationToken)
        {
            var res = await _statisticService.GetTopLatestTransaction(request);
            foreach (var item in res)
            {
                var userResp = await _authClientAPI.GetUser(item.UserId);
                if (userResp == null || userResp.Data == null)
                {
                    throw new InternalServiceCommunicationException("Cannot get user info");
                }
                item.User = userResp.Data;
            }

            return res;
        }
    }
}
