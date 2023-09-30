using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Services.Order.Application.Dtos;
using SS_Microservice.Services.Order.Application.Features.Transaction.Queries;

namespace SS_Microservice.Services.Order.Application.Interfaces
{
    public interface ITransactionService
    {
        Task<PaginatedResult<TransactionDto>> GetTransactionList(GetAllTransactionQuery query);

        Task<TransactionDto> GetTransaction(GetTransactionByIdQuery query);
    }
}