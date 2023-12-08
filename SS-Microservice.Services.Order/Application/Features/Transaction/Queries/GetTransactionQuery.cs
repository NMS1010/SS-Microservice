using MediatR;
using SS_Microservice.Services.Order.Application.Dtos;
using SS_Microservice.Services.Order.Application.Interfaces;

namespace SS_Microservice.Services.Order.Application.Features.Transaction.Queries
{
    public class GetTransactionQuery : IRequest<TransactionDto>
    {
        public long Id { get; set; }
    }

    public class GetTransactionByIdHandler : IRequestHandler<GetTransactionQuery, TransactionDto>
    {
        private readonly ITransactionService _transactionService;

        public GetTransactionByIdHandler(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        public async Task<TransactionDto> Handle(GetTransactionQuery request, CancellationToken cancellationToken)
        {
            return await _transactionService.GetTransaction(request);
        }
    }
}