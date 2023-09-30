using MediatR;
using SS_Microservice.Services.Order.Application.Dtos;
using SS_Microservice.Services.Order.Application.Interfaces;

namespace SS_Microservice.Services.Order.Application.Features.Transaction.Queries
{
    public class GetTransactionByIdQuery : IRequest<TransactionDto>
    {
        public long Id { get; set; }
    }

    public class GetTransactionByIdHandler : IRequestHandler<GetTransactionByIdQuery, TransactionDto>
    {
        private readonly ITransactionService _transactionService;

        public GetTransactionByIdHandler(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        public async Task<TransactionDto> Handle(GetTransactionByIdQuery request, CancellationToken cancellationToken)
        {
            return await _transactionService.GetTransaction(request);
        }
    }
}