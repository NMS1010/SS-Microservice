using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Services.Auth.Application.Model.CustomResponse;
using SS_Microservice.Services.Order.Application.Dtos;
using SS_Microservice.Services.Order.Application.Features.OrderState.Commands;
using SS_Microservice.Services.Order.Application.Features.OrderState.Queries;
using SS_Microservice.Services.Order.Application.Models.OrderState;

namespace SS_Microservice.Services.Order.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderStatesController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public OrderStatesController(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllOrderStates([FromQuery] GetOrderStatePagingRequest request)
        {
            var query = _mapper.Map<GetAllOrderStateQuery>(request);

            var res = await _sender.Send(query);

            return Ok(CustomAPIResponse<PaginatedResult<OrderStateDto>>.Success(res, StatusCodes.Status200OK));
        }

        [HttpGet("detail/{orderStateId}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> GetOrderStateById([FromRoute] long orderStateId)
        {
            var query = new GetOrderStateByIdQuery()
            {
                OrderStateId = orderStateId
            };
            var res = await _sender.Send(query);

            return Ok(CustomAPIResponse<OrderStateDto>.Success(res, StatusCodes.Status200OK));
        }

        [HttpPost("create")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> CreateOrderState([FromBody] CreateOrderStateRequest request)
        {
            var command = _mapper.Map<CreateOrderStateCommand>(request);
            await _sender.Send(command);

            return Ok(CustomAPIResponse<NoContentAPIResponse>.Success(StatusCodes.Status201Created));
        }

        [HttpPut("update")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> UpdateOrderState([FromBody] UpdateOrderStateRequest request)
        {
            var command = _mapper.Map<UpdateOrderStateCommand>(request);
            var isSuccess = await _sender.Send(command);
            if (!isSuccess)
            {
                return Ok(CustomAPIResponse<NoContentAPIResponse>.Fail(StatusCodes.Status400BadRequest, "Cannot update this orderState"));
            }

            return Ok(CustomAPIResponse<NoContentAPIResponse>.Success(StatusCodes.Status204NoContent));
        }

        [HttpDelete("delete/{orderStateId}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> DeleteOrderState(long orderStateId)
        {
            var command = new DeleteOrderStateCommand() { OrderStateId = orderStateId };
            var isSuccess = await _sender.Send(command);
            if (!isSuccess)
            {
                return Ok(CustomAPIResponse<NoContentAPIResponse>.Fail(StatusCodes.Status400BadRequest, "Cannot delete this orderState"));
            }

            return Ok(CustomAPIResponse<NoContentAPIResponse>.Success(StatusCodes.Status204NoContent));
        }
    }
}