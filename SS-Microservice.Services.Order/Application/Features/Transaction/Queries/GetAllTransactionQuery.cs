using MediatR;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Services.Order.Application.Dtos;
using SS_Microservice.Services.Order.Application.Interfaces;
using SS_Microservice.Services.Order.Application.Models.Transaction;

namespace SS_Microservice.Services.Order.Application.Features.Transaction.Queries
{
    public class GetAllTransactionQuery : GetTransactionPagingRequest, IRequest<PaginatedResult<TransactionDto>>
    {
    }

    public class GetAllTransactionHandler : IRequestHandler<GetAllTransactionQuery, PaginatedResult<TransactionDto>>
    {
        private readonly ITransactionService _transactionService;

        public GetAllTransactionHandler(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        public async Task<PaginatedResult<TransactionDto>> Handle(GetAllTransactionQuery request, CancellationToken cancellationToken)
        {
            return await _transactionService.GetTransactionList(request);
        }
    }
}