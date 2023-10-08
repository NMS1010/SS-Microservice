using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Services.Auth.Application.Model.CustomResponse;
using SS_Microservice.Services.Order.Application.Dtos;
using SS_Microservice.Services.Order.Application.Features.PaymentMethod.Commands;
using SS_Microservice.Services.Order.Application.Features.PaymentMethod.Queries;
using SS_Microservice.Services.Order.Application.Models.PaymentMethod;

namespace SS_Microservice.Services.Order.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PaymentMethodsController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public PaymentMethodsController(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllPaymentMethods([FromQuery] GetPaymentMethodPagingRequest request)
        {
            var query = _mapper.Map<GetAllPaymentMethodQuery>(request);

            var res = await _sender.Send(query);

            return Ok(CustomAPIResponse<PaginatedResult<PaymentMethodDto>>.Success(res, StatusCodes.Status200OK));
        }

        [HttpGet("detail/{paymentMethodId}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> GetOrderById([FromRoute] long paymentMethodId)
        {
            var query = new GetPaymentMethodByIdQuery()
            {
                Id = paymentMethodId
            };
            var res = await _sender.Send(query);

            return Ok(CustomAPIResponse<PaymentMethodDto>.Success(res, StatusCodes.Status200OK));
        }

        [HttpPost("create")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> CreatePaymentMethod([FromForm] CreatePaymentMethodRequest request)
        {
            var command = _mapper.Map<CreatePaymentMethodCommand>(request);
            var isSuccess = await _sender.Send(command);
            return Ok(CustomAPIResponse<bool>.Success(isSuccess, StatusCodes.Status201Created));
        }

        [HttpPut("update")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> UpdatePaymentMethod([FromForm] UpdatePaymentMethodRequest request)
        {
            var command = _mapper.Map<UpdatePaymentMethodCommand>(request);
            var isSuccess = await _sender.Send(command);

            return Ok(CustomAPIResponse<bool>.Success(isSuccess, StatusCodes.Status204NoContent));
        }

        [HttpDelete("delete/{paymentMethodId}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> DeletePaymentMethod(long paymentMethodId)
        {
            var command = new DeletePaymentMethodCommand() { Id = paymentMethodId };
            var isSuccess = await _sender.Send(command);
            return Ok(CustomAPIResponse<bool>.Success(isSuccess, StatusCodes.Status204NoContent));
        }
    }
}