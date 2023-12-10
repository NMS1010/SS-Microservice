using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SS_Microservice.Common.Types.Model.CustomResponse;
using SS_Microservice.Common.Types.Model.Paging;
using SS_Microservice.Services.Order.Application.Dtos;
using SS_Microservice.Services.Order.Application.Features.OrderCancellationReason.Commands;
using SS_Microservice.Services.Order.Application.Features.OrderCancellationReason.Queries;
using SS_Microservice.Services.Order.Application.Models.OrderCancellationReason;

namespace SS_Microservice.Services.Order.Controllers
{
    [Route("api/order-cancel-reasons")]
    [ApiController]
    [Authorize(Roles = "ADMIN")]
    public class OrderCancellationReasonsController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public OrderCancellationReasonsController(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetListOrderCancellationReason([FromQuery] GetOrderCancellationReasonPagingRequest request)
        {
            var resp = await _sender.Send(_mapper.Map<GetListOrderCancellationReasonQuery>(request));

            return Ok(CustomAPIResponse<PaginatedResult<OrderCancellationReasonDto>>.Success(resp, StatusCodes.Status200OK));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderCancellationReason([FromRoute] long id)
        {
            var resp = await _sender.Send(new GetOrderCancellationReasonQuery() { Id = id });

            return Ok(CustomAPIResponse<OrderCancellationReasonDto>.Success(resp, StatusCodes.Status200OK));
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrderCancellationReason([FromBody] CreateOrderCancellationReasonRequest request)
        {
            var id = await _sender.Send(_mapper.Map<CreateOrderCancellationReasonCommand>(request));

            var url = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/api/ordercancellationreasons/{id}";

            return Created(url, CustomAPIResponse<object>.Success(new { id }, StatusCodes.Status201Created));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrderCancellationReason([FromRoute] long id, [FromBody] UpdateOrderCancellationReasonRequest request)
        {
            request.Id = id;
            var resp = await _sender.Send(_mapper.Map<UpdateOrderCancellationReasonCommand>(request));

            return Ok(CustomAPIResponse<bool>.Success(resp, StatusCodes.Status204NoContent));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderCancellationReason([FromRoute] long id)
        {
            var resp = await _sender.Send(new DeleteOrderCancellationReasonCommand() { Id = id });

            return Ok(CustomAPIResponse<bool>.Success(resp, StatusCodes.Status204NoContent));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteListOrderCancellationReason([FromQuery] List<long> ids)
        {
            var resp = await _sender.Send(new DeleteListOrderCancellationReasonCommand() { Ids = ids });

            return Ok(CustomAPIResponse<bool>.Success(resp, StatusCodes.Status204NoContent));
        }
    }
}