using MediatR;
using SS_Microservice.Services.Order.Application.Dtos;
using SS_Microservice.Services.Order.Application.Interfaces;

namespace SS_Microservice.Services.Order.Application.Features.Transaction.Queries
{
    public class GetTopLatestTransactionQuery : IRequest<List<StatisticTransactionDto>>
    {
        public int Top { get; set; } = 5;
    }

    public class GetTopLatestTransactionHandler : IRequestHandler<GetTopLatestTransactionQuery, List<StatisticTransactionDto>>
    {
        private readonly ITransactionService _transactionService;

        public GetTopLatestTransactionHandler(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        public async Task<List<StatisticTransactionDto>> Handle(GetTopLatestTransactionQuery request, CancellationToken cancellationToken)
        {
            return await _transactionService.GetTopLatestTransaction(request);
        }
    }
}
