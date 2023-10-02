using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Common.Services.CurrentUser;
using SS_Microservice.Services.Address.Application.Dto;
using SS_Microservice.Services.Address.Application.Features.Address.Commands;
using SS_Microservice.Services.Address.Application.Features.Address.Queries;
using SS_Microservice.Services.Address.Application.Models.Address;
using SS_Microservice.Services.Auth.Application.Model.CustomResponse;

namespace SS_Microservice.Services.Address.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AddressesController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public AddressesController(ISender sender, IMapper mapper, ICurrentUserService currentUserService)
        {
            _sender = sender;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllAddresses([FromQuery] GetAddressPagingRequest request)
        {
            var query = _mapper.Map<GetAllAddressQuery>(request);
            query.UserId = _currentUserService.UserId;
            var res = await _sender.Send(query);

            return Ok(CustomAPIResponse<PaginatedResult<AddressDto>>.Success(res, StatusCodes.Status200OK));
        }

        [HttpGet("detail/{addressId}")]
        public async Task<IActionResult> GetAddressById([FromRoute] int addressId)
        {
            var query = new GetAddressByIdQuery()
            {
                Id = addressId,
                UserId = _currentUserService.UserId
            };
            var res = await _sender.Send(query);

            return Ok(CustomAPIResponse<AddressDto>.Success(res, StatusCodes.Status200OK));
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAddress([FromForm] CreateAddressRequest request)
        {
            var command = _mapper.Map<CreateAddressCommand>(request);
            command.UserId = _currentUserService.UserId;
            await _sender.Send(command);

            return Ok(CustomAPIResponse<NoContentAPIResponse>.Success(StatusCodes.Status201Created));
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateAddress([FromForm] UpdateAddressRequest request)
        {
            var command = _mapper.Map<UpdateAddressCommand>(request);
            command.UserId = _currentUserService.UserId;
            var isSuccess = await _sender.Send(command);
            if (!isSuccess)
            {
                return Ok(CustomAPIResponse<NoContentAPIResponse>.Fail(StatusCodes.Status400BadRequest, "Cannot update this address"));
            }

            return Ok(CustomAPIResponse<NoContentAPIResponse>.Success(StatusCodes.Status204NoContent));
        }

        [HttpDelete("delete/{addressId}")]
        public async Task<IActionResult> DeleteAddress(long addressId)
        {
            var command = new DeleteAddressCommand() { Id = addressId, UserId = _currentUserService.UserId };
            var isSuccess = await _sender.Send(command);
            if (!isSuccess)
            {
                return Ok(CustomAPIResponse<NoContentAPIResponse>.Fail(StatusCodes.Status400BadRequest, "Cannot delete this address"));
            }

            return Ok(CustomAPIResponse<NoContentAPIResponse>.Success(StatusCodes.Status204NoContent));
        }
    }
}