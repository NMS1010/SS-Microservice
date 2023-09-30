using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Services.Auth.Application.Model.CustomResponse;
using SS_Microservice.Services.Order.Application.Dtos;
using SS_Microservice.Services.Order.Application.Features.Order.Queries;
using SS_Microservice.Services.Order.Application.Features.Transaction.Queries;
using SS_Microservice.Services.Order.Application.Models.Order;
using SS_Microservice.Services.Order.Application.Models.Transaction;

namespace SS_Microservice.Services.Order.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "ADMIN")]
    public class TransactionsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ISender _sender;

        public TransactionsController(IMapper mapper, ISender sender)
        {
            _mapper = mapper;
            _sender = sender;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllTransactions([FromQuery] GetTransactionPagingRequest request)
        {
            var query = _mapper.Map<GetAllTransactionQuery>(request);

            var res = await _sender.Send(query);

            return Ok(CustomAPIResponse<PaginatedResult<TransactionDto>>.Success(res, StatusCodes.Status200OK));
        }

        [HttpGet("detail/{transactionId}")]
        public async Task<IActionResult> GetTransactionById([FromRoute] long transactionId)
        {
            var query = new GetTransactionByIdQuery()
            {
                Id = transactionId
            };
            var res = await _sender.Send(query);

            return Ok(CustomAPIResponse<TransactionDto>.Success(res, StatusCodes.Status200OK));
        }
    }
}