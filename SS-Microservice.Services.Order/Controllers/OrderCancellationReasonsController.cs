using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Services.Auth.Application.Model.CustomResponse;
using SS_Microservice.Services.Order.Application.Dtos;
using SS_Microservice.Services.Order.Application.Features.OrderCancellationReason.Commands;
using SS_Microservice.Services.Order.Application.Features.OrderCancellationReason.Queries;
using SS_Microservice.Services.Order.Application.Models.OrderCancellationReason;

namespace SS_Microservice.Services.Order.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderCancellationReasonsController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public OrderCancellationReasonsController(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllOrderCancellationReasons([FromQuery] GetOrderCancellationReasonPagingRequest request)
        {
            var query = _mapper.Map<GetAllOrderCancellationReasonQuery>(request);

            var res = await _sender.Send(query);

            return Ok(CustomAPIResponse<PaginatedResult<OrderCancellationReasonDto>>.Success(res, StatusCodes.Status200OK));
        }

        [HttpGet("detail/{orderCancellationReasonId}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> GetOrderOrderCancellationReasonById([FromRoute] long orderCancellationReasonId)
        {
            var query = new GetOrderCancellationReasonByIdQuery()
            {
                Id = orderCancellationReasonId
            };
            var res = await _sender.Send(query);

            return Ok(CustomAPIResponse<OrderCancellationReasonDto>.Success(res, StatusCodes.Status200OK));
        }

        [HttpPost("create")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> CreateOrderCancellationReason([FromBody] CreateOrderCancellationReasonRequest request)
        {
            var command = _mapper.Map<CreateOrderCancellationReasonCommand>(request);
            var isSuccess = await _sender.Send(command);

            return Ok(CustomAPIResponse<bool>.Success(isSuccess, StatusCodes.Status201Created));
        }

        [HttpPut("update")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> UpdateOrderCancellationReason([FromBody] UpdateOrderCancellationReasonRequest request)
        {
            var command = _mapper.Map<UpdateOrderCancellationReasonCommand>(request);
            var isSuccess = await _sender.Send(command);

            return Ok(CustomAPIResponse<bool>.Success(isSuccess, StatusCodes.Status204NoContent));
        }

        [HttpDelete("delete/{orderCancellationReasonId}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> DeleteOrderCancellationReason(long orderCancellationReasonId)
        {
            var command = new DeleteOrderCancellationReasonCommand() { Id = orderCancellationReasonId };
            var isSuccess = await _sender.Send(command);

            return Ok(CustomAPIResponse<bool>.Success(isSuccess, StatusCodes.Status204NoContent));
        }
    }
}