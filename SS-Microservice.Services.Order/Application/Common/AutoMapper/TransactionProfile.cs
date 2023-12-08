using AutoMapper;
using SS_Microservice.Services.Order.Application.Dtos;
using SS_Microservice.Services.Order.Application.Features.Transaction.Queries;
using SS_Microservice.Services.Order.Application.Models.Transaction;
using SS_Microservice.Services.Order.Domain.Entities;

namespace SS_Microservice.Services.Order.Application.Common.AutoMapper
{
    public class TransactionProfile : Profile
    {
        public TransactionProfile()
        {
            CreateMap<Transaction, TransactionDto>();


            // mapping request - command
            CreateMap<GetTransactionPagingRequest, GetListTransactionQuery>();
        }
    }
}
