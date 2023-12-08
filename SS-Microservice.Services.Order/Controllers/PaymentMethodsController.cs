using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Services.Auth.Application.Model.CustomResponse;
using SS_Microservice.Services.Order.Application.Dtos;
using SS_Microservice.Services.Order.Application.Features.PaymentMethod.Commands;
using SS_Microservice.Services.Order.Application.Features.PaymentMethod.Queries;
using SS_Microservice.Services.Order.Application.Models.PaymentMethod;

namespace SS_Microservice.Services.Order.Controllers
{
    [Route("api/payment-methods")]
    [ApiController]
    [Authorize(Roles = "ADMIN")]
    public class PaymentMethodsController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public PaymentMethodsController(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetListPaymentMethod([FromQuery] GetPaymentMethodPagingRequest request)
        {
            var resp = await _sender.Send(_mapper.Map<GetListPaymentMethodQuery>(request));

            return Ok(CustomAPIResponse<PaginatedResult<PaymentMethodDto>>.Success(resp, StatusCodes.Status200OK));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPaymentMethod([FromRoute] long id)
        {
            var resp = await _sender.Send(new GetPaymentMethodQuery() { Id = id });

            return Ok(CustomAPIResponse<PaymentMethodDto>.Success(resp, StatusCodes.Status200OK));
        }

        [HttpPost]
        public async Task<IActionResult> CreatePaymentMethod([FromForm] CreatePaymentMethodRequest request)
        {
            var paymentMethodId = await _sender.Send(_mapper.Map<CreatePaymentMethodCommand>(request));

            var url = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/api/paymentmethods/{paymentMethodId}";

            return Created(url, CustomAPIResponse<object>.Success(new { id = paymentMethodId }, StatusCodes.Status201Created));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePaymentMethod([FromRoute] long id, [FromForm] UpdatePaymentMethodRequest request)
        {
            request.Id = id;
            var resp = await _sender.Send(_mapper.Map<UpdatePaymentMethodCommand>(request));

            return Ok(CustomAPIResponse<bool>.Success(resp, StatusCodes.Status204NoContent));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaymentMethod([FromRoute] long id)
        {
            var resp = await _sender.Send(new DeletePaymentMethodCommand() { Id = id });

            return Ok(CustomAPIResponse<bool>.Success(resp, StatusCodes.Status204NoContent));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteListPaymentMethod([FromQuery] List<long> ids)
        {
            var resp = await _sender.Send(new DeleteListPaymentMethodCommand() { Ids = ids });

            return Ok(CustomAPIResponse<bool>.Success(resp, StatusCodes.Status204NoContent));
        }
    }
}