using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Services.Auth.Application.Model.CustomResponse;
using SS_Microservice.Services.Order.Application.Dtos;
using SS_Microservice.Services.Order.Application.Features.Delivery.Commands;
using SS_Microservice.Services.Order.Application.Features.Delivery.Queries;
using SS_Microservice.Services.Order.Application.Models.Delivery;

namespace SS_Microservice.Services.Order.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DeliveriesController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public DeliveriesController(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllDeliveries([FromQuery] GetDeliveryPagingRequest request)
        {
            var query = _mapper.Map<GetAllDeliveryQuery>(request);

            var res = await _sender.Send(query);

            return Ok(CustomAPIResponse<PaginatedResult<DeliveryDto>>.Success(res, StatusCodes.Status200OK));
        }

        [HttpGet("detail/{deliveryId}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> GetDeliveryById([FromRoute] int deliveryId)
        {
            var query = new GetDeliveryByIdQuery()
            {
                Id = deliveryId
            };
            var res = await _sender.Send(query);

            return Ok(CustomAPIResponse<DeliveryDto>.Success(res, StatusCodes.Status200OK));
        }

        [HttpPost("create")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> CreateDelivery([FromForm] CreateDeliveryRequest request)
        {
            var command = _mapper.Map<CreateDeliveryCommand>(request);
            await _sender.Send(command);

            return Ok(CustomAPIResponse<NoContentAPIResponse>.Success(StatusCodes.Status201Created));
        }

        [HttpPut("update")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> UpdateDelivery([FromForm] UpdateDeliveryRequest request)
        {
            var command = _mapper.Map<UpdateDeliveryCommand>(request);
            var isSuccess = await _sender.Send(command);
            if (!isSuccess)
            {
                return Ok(CustomAPIResponse<NoContentAPIResponse>.Fail(StatusCodes.Status400BadRequest, "Cannot update this delivery"));
            }

            return Ok(CustomAPIResponse<NoContentAPIResponse>.Success(StatusCodes.Status204NoContent));
        }

        [HttpDelete("delete/{deliveryId}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> DeleteDelivery(int deliveryId)
        {
            var command = new DeleteDeliveryCommand() { Id = deliveryId };
            var isSuccess = await _sender.Send(command);
            if (!isSuccess)
            {
                return Ok(CustomAPIResponse<NoContentAPIResponse>.Fail(StatusCodes.Status400BadRequest, "Cannot delete this delivery"));
            }

            return Ok(CustomAPIResponse<NoContentAPIResponse>.Success(StatusCodes.Status204NoContent));
        }
    }
}