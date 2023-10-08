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
using SS_Microservice.Services.Address.Application.Features.District.Queries;
using SS_Microservice.Services.Address.Application.Features.Province.Queries;
using SS_Microservice.Services.Address.Application.Features.Ward.Queries;
using SS_Microservice.Services.Address.Application.Models.Address;
using SS_Microservice.Services.Address.Application.Models.District;
using SS_Microservice.Services.Address.Application.Models.Province;
using SS_Microservice.Services.Address.Application.Models.Ward;
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
            var res = await _sender.Send(command);

            return Ok(CustomAPIResponse<bool>.Success(res, StatusCodes.Status201Created));
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateAddress([FromForm] UpdateAddressRequest request)
        {
            var command = _mapper.Map<UpdateAddressCommand>(request);
            command.UserId = _currentUserService.UserId;
            var isSuccess = await _sender.Send(command);

            return Ok(CustomAPIResponse<bool>.Success(isSuccess, StatusCodes.Status204NoContent));
        }

        [HttpDelete("delete/{addressId}")]
        public async Task<IActionResult> DeleteAddress(long addressId)
        {
            var command = new DeleteAddressCommand() { Id = addressId, UserId = _currentUserService.UserId };
            var isSuccess = await _sender.Send(command);

            return Ok(CustomAPIResponse<bool>.Success(isSuccess, StatusCodes.Status204NoContent));
        }

        // province, district, ward

        [HttpGet("p/all")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllProvince([FromQuery] GetProvincePagingRequest request)
        {
            var query = _mapper.Map<GetAllProvinceQuery>(request);

            var res = await _sender.Send(query);

            return Ok(CustomAPIResponse<PaginatedResult<ProvinceDto>>.Success(res, StatusCodes.Status200OK));
        }

        [HttpGet("p/d/all")]
        [AllowAnonymous]
        public async Task<IActionResult> GetDistrictByProvinceId([FromQuery] GetDistrictPagingRequest request)
        {
            var query = _mapper.Map<GetDistrictByProvinceIdQuery>(request);

            var res = await _sender.Send(query);

            return Ok(CustomAPIResponse<PaginatedResult<DistrictDto>>.Success(res, StatusCodes.Status200OK));
        }

        [HttpGet("p/d/w/all")]
        [AllowAnonymous]
        public async Task<IActionResult> GetWardByDistrictId([FromQuery] GetWardPagingRequest request)
        {
            var query = _mapper.Map<GetWardByDistrictIdQuery>(request);

            var res = await _sender.Send(query);

            return Ok(CustomAPIResponse<PaginatedResult<WardDto>>.Success(res, StatusCodes.Status200OK));
        }
    }
}