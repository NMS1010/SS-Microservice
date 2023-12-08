using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "ADMIN")]
    public class DeliveriesController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public DeliveriesController(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetListDelivery([FromQuery] GetDeliveryPagingRequest request)
        {
            var resp = await _sender.Send(_mapper.Map<GetListDeliveryQuery>(request));

            return Ok(CustomAPIResponse<PaginatedResult<DeliveryDto>>.Success(resp, StatusCodes.Status200OK));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDelivery([FromRoute] long id)
        {
            var resp = await _sender.Send(new GetDeliveryQuery() { Id = id });

            return Ok(CustomAPIResponse<DeliveryDto>.Success(resp, StatusCodes.Status200OK));
        }

        [HttpPost]
        public async Task<IActionResult> CreateDelivery([FromForm] CreateDeliveryRequest request)
        {
            var deliveryId = await _sender.Send(_mapper.Map<CreateDeliveryCommand>(request));

            var url = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/api/deliveries/{deliveryId}";

            return Created(url, CustomAPIResponse<object>.Success(new { id = deliveryId }, StatusCodes.Status201Created));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDelivery([FromRoute] long id, [FromForm] UpdateDeliveryRequest request)
        {
            request.Id = id;
            var resp = await _sender.Send(_mapper.Map<UpdateDeliveryCommand>(request));

            return Ok(CustomAPIResponse<bool>.Success(resp, StatusCodes.Status204NoContent));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDelivery([FromRoute] long id)
        {
            var resp = await _sender.Send(new DeleteDeliveryCommand() { Id = id });

            return Ok(CustomAPIResponse<bool>.Success(resp, StatusCodes.Status204NoContent));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteListDelivery([FromQuery] List<long> ids)
        {
            var resp = await _sender.Send(new DeleteListDeliveryCommand() { Ids = ids });

            return Ok(CustomAPIResponse<bool>.Success(resp, StatusCodes.Status204NoContent));
        }
    }
}