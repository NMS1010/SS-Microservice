using AutoMapper;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Common.Repository;
using SS_Microservice.Services.Order.Application.Dtos;
using SS_Microservice.Services.Order.Application.Features.Transaction.Queries;
using SS_Microservice.Services.Order.Application.Interfaces;
using SS_Microservice.Services.Order.Application.Specifications;
using SS_Microservice.Services.Order.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace SS_Microservice.Services.Order.Infrastructure.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public TransactionService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<TransactionDto> GetTransaction(GetTransactionByIdQuery query)
        {
            var spec = new TransactionSpecification(query.Id);
            var transaction = await _unitOfWork.Repository<Transaction>().GetEntityWithSpec(spec);

            return _mapper.Map<TransactionDto>(transaction);
        }

        public async Task<PaginatedResult<TransactionDto>> GetTransactionList(GetAllTransactionQuery query)
        {
            var spec = new TransactionSpecification(query, isPaging: true);

            var transactions = await _unitOfWork.Repository<Transaction>().ListAsync(spec);

            var transactionDtos = new List<TransactionDto>();
            transactions.ForEach(x => transactionDtos.Add(_mapper.Map<TransactionDto>(x)));

            var specCount = new TransactionSpecification(query);

            var totalCount = await _unitOfWork.Repository<Transaction>().CountAsync(specCount);

            return new PaginatedResult<TransactionDto>(transactionDtos, (int)query.PageIndex, totalCount, (int)query.PageSize);
        }
    }
}