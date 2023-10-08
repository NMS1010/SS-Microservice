using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Common.Services.CurrentUser;
using SS_Microservice.Services.Auth.Application.Model.CustomResponse;
using SS_Microservice.Services.Order.Application.Dtos;
using SS_Microservice.Services.Order.Application.Features.Order.Commands;
using SS_Microservice.Services.Order.Application.Features.Order.Queries;
using SS_Microservice.Services.Order.Application.Models.Order;

namespace SS_Microservice.Services.Order.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public OrdersController(ISender sender, IMapper mapper, ICurrentUserService currentUserService)
        {
            _sender = sender;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        [HttpGet("all")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> GetAllOrders([FromQuery] GetOrderPagingRequest request)
        {
            var query = _mapper.Map<GetAllOrderQuery>(request);

            var res = await _sender.Send(query);

            return Ok(CustomAPIResponse<PaginatedResult<OrderDto>>.Success(res, StatusCodes.Status200OK));
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetUserOrders([FromQuery] GetOrderPagingRequest request)
        {
            var query = _mapper.Map<GetAllOrderQuery>(request);
            query.UserId = _currentUserService.UserId;
            var res = await _sender.Send(query);

            return Ok(CustomAPIResponse<PaginatedResult<OrderDto>>.Success(res, StatusCodes.Status200OK));
        }

        [HttpGet("detail/{orderId}")]
        public async Task<IActionResult> GetOrderById([FromRoute] long orderId)
        {
            var query = new GetOrderByIdQuery()
            {
                OrderId = orderId,
                UserId = _currentUserService.UserId
            };
            var res = await _sender.Send(query);

            return Ok(CustomAPIResponse<OrderDto>.Success(res, StatusCodes.Status200OK));
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
        {
            var command = _mapper.Map<CreateOrderCommand>(request);
            command.UserId = _currentUserService.UserId;
            var success = await _sender.Send(command);

            return Ok(CustomAPIResponse<bool>.Success(success, StatusCodes.Status201Created));
        }

        [HttpPut("update")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> UpdateOrder([FromBody] UpdateOrderRequest request)
        {
            var command = _mapper.Map<UpdateOrderCommand>(request);
            var success = await _sender.Send(command);

            return Ok(CustomAPIResponse<bool>.Success(success, StatusCodes.Status204NoContent));
        }
    }
}