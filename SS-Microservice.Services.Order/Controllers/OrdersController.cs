using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
        {
            request.UserId = _currentUserService.UserId;
            var code = await _sender.Send(_mapper.Map<CreateOrderCommand>(request));

            var url = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/api/orders/detail/{code}";

            return Created(url, CustomAPIResponse<object>.Success(new { code }, StatusCodes.Status201Created));
        }

        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> GetListOrder([FromQuery] GetOrderPagingRequest request)
        {
            request.UserId = null;
            var orders = await _sender.Send(_mapper.Map<GetListOrderQuery>(request));

            return Ok(CustomAPIResponse<PaginatedResult<OrderDto>>.Success(orders, StatusCodes.Status200OK));
        }

        [HttpGet("me/list")]
        public async Task<IActionResult> GetListUserOrder([FromQuery] GetOrderPagingRequest request)
        {
            request.UserId = _currentUserService.UserId;
            var orders = await _sender.Send(_mapper.Map<GetListUserOrderQuery>(request));

            return Ok(CustomAPIResponse<PaginatedResult<OrderDto>>.Success(orders, StatusCodes.Status200OK));
        }

        [HttpGet("top5-order-latest")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> GetTop5OrderLatest()
        {
            var resp = await _sender.Send(new GetTopLatestOrderQuery());

            return Ok(CustomAPIResponse<List<OrderDto>>.Success(resp, StatusCodes.Status200OK));
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> GetOrder([FromRoute] long id)
        {
            var order = await _sender.Send(new GetOrderQuery() { Id = id });

            return Ok(CustomAPIResponse<OrderDto>.Success(order, StatusCodes.Status200OK));
        }

        [HttpGet("detail/{code}")]
        public async Task<IActionResult> GetOrderByCode([FromRoute] string code)
        {
            var order = await _sender.Send(new GetOrderByCodeQuery() { Code = code, UserId = _currentUserService.UserId });

            return Ok(CustomAPIResponse<OrderDto>.Success(order, StatusCodes.Status200OK));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder([FromRoute] long id, [FromBody] UpdateOrderRequest request)
        {
            request.OrderId = id;
            if (!_currentUserService.IsInRole("ADMIN"))
                request.UserId = _currentUserService.UserId;

            var isSuccess = await _sender.Send(_mapper.Map<UpdateOrderCommand>(request));

            return Ok(CustomAPIResponse<bool>.Success(isSuccess, StatusCodes.Status204NoContent));
        }

        [HttpPut("paypal/{id}")]
        public async Task<IActionResult> CompletePaypalOrder([FromRoute] long id, [FromBody] CompletePaypalOrderRequest request)
        {
            request.OrderId = id;
            request.UserId = _currentUserService.UserId;

            var isSuccess = await _sender.Send(_mapper.Map<CompletePaypalOrderCommand>(request));

            return Ok(CustomAPIResponse<bool>.Success(isSuccess, StatusCodes.Status204NoContent));
        }
    }
}