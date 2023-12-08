using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Services.Auth.Application.Model.CustomResponse;
using SS_Microservice.Services.Order.Application.Dtos;
using SS_Microservice.Services.Order.Application.Features.Transaction.Queries;
using SS_Microservice.Services.Order.Application.Models.Transaction;

namespace SS_Microservice.Services.Order.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "ADMIN")]
    public class TransactionsController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public TransactionsController(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetListTranssaction([FromQuery] GetTransactionPagingRequest request)
        {
            var transactions = await _sender.Send(_mapper.Map<GetListTransactionQuery>(request));

            return Ok(CustomAPIResponse<PaginatedResult<TransactionDto>>.Success(transactions, StatusCodes.Status200OK));
        }

        [HttpGet("top5-tracsaction-latest")]
        public async Task<IActionResult> GetTop5TransactionLatest()
        {
            var transactions = await _sender.Send(new GetTopLatestTransactionQuery());

            return Ok(CustomAPIResponse<List<StatisticTransactionDto>>.Success(transactions, StatusCodes.Status200OK));
        }

        [HttpGet("{transactionId}")]
        public async Task<IActionResult> GetTranssaction([FromRoute] long transactionId)
        {
            var transaction = await _sender.Send(new GetTransactionQuery() { Id = transactionId });

            return Ok(CustomAPIResponse<TransactionDto>.Success(transaction, StatusCodes.Status200OK));
        }
    }
}