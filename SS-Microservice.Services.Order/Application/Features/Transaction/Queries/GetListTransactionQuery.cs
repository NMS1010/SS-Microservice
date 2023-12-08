using MediatR;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Services.Order.Application.Dtos;
using SS_Microservice.Services.Order.Application.Interfaces;
using SS_Microservice.Services.Order.Application.Models.Transaction;

namespace SS_Microservice.Services.Order.Application.Features.Transaction.Queries
{
    public class GetListTransactionQuery : GetTransactionPagingRequest, IRequest<PaginatedResult<TransactionDto>>
    {
    }

    public class GetAllTransactionHandler : IRequestHandler<GetListTransactionQuery, PaginatedResult<TransactionDto>>
    {
        private readonly ITransactionService _transactionService;

        public GetAllTransactionHandler(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        public async Task<PaginatedResult<TransactionDto>> Handle(GetListTransactionQuery request, CancellationToken cancellationToken)
        {
            return await _transactionService.GetListTransaction(request);
        }
    }
}