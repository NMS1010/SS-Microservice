using AutoMapper;
using SS_Microservice.Common.Repository;
using SS_Microservice.Common.Types.Model.Paging;
using SS_Microservice.Services.Order.Application.Dtos;
using SS_Microservice.Services.Order.Application.Features.Transaction.Queries;
using SS_Microservice.Services.Order.Application.Interfaces;
using SS_Microservice.Services.Order.Application.Specifications.Transaction;

namespace SS_Microservice.Services.Order.Application.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TransactionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<TransactionDto>> GetListTransaction(GetListTransactionQuery query)
        {
            var transactions = await _unitOfWork.Repository<Domain.Entities.Transaction>()
                .ListAsync(new TransactionSpecification(query, isPaging: true));
            var count = await _unitOfWork.Repository<Domain.Entities.Transaction>()
                .CountAsync(new TransactionSpecification(query));

            return new PaginatedResult<TransactionDto>(transactions.Select(x =>
            {
                var res = _mapper.Map<TransactionDto>(x);
                res.OrderCode = x.Order.Code;
                return res;
            }).ToList(),
                query.PageIndex, count, query.PageSize);
        }

        public async Task<TransactionDto> GetTransaction(GetTransactionQuery query)
        {
            var transaction = await _unitOfWork.Repository<Domain.Entities.Transaction>()
                .GetEntityWithSpec(new TransactionSpecification(query.Id));

            var dto = _mapper.Map<TransactionDto>(transaction);
            dto.OrderCode = transaction.Order.Code;

            return dto;
        }
    }
}