using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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

        [HttpPost]
        public async Task<IActionResult> CreateAddress([FromBody] CreateAddressRequest request)
        {
            var command = _mapper.Map<CreateAddressCommand>(request);
            command.UserId = _currentUserService.UserId;

            long addressId = await _sender.Send(command);

            return Ok(CustomAPIResponse<object>.Success(new { id = addressId }, StatusCodes.Status201Created));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAddress([FromRoute] long id, [FromBody] UpdateAddressRequest request)
        {
            var command = _mapper.Map<UpdateAddressCommand>(request);
            command.UserId = _currentUserService.UserId;
            command.Id = id;
            var res = await _sender.Send(command);

            return Ok(CustomAPIResponse<bool>.Success(res, StatusCodes.Status200OK));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAddress([FromRoute] long id)
        {
            var query = new GetAddressQuery() { Id = id, UserId = _currentUserService.UserId };

            var res = await _sender.Send(query);

            return Ok(CustomAPIResponse<AddressDto>.Success(res, StatusCodes.Status200OK));
        }

        [HttpGet("default")]
        public async Task<IActionResult> GetDefaultAddress()
        {
            var query = new GetDefaultAddressQuery() { UserId = _currentUserService.UserId };
            var res = await _sender.Send(query);

            return Ok(CustomAPIResponse<AddressDto>.Success(res, StatusCodes.Status200OK));
        }

        [HttpGet]
        public async Task<IActionResult> GetListAddress([FromQuery] GetAddressPagingRequest request)
        {
            request.UserId = _currentUserService.UserId;
            var query = _mapper.Map<GetListAddressQuery>(request);

            var res = await _sender.Send(query);

            return Ok(CustomAPIResponse<PaginatedResult<AddressDto>>.Success(res, StatusCodes.Status200OK));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress([FromRoute] long id)
        {
            var command = new DeleteAddressCommand() { Id = id, UserId = _currentUserService.UserId };
            var res = await _sender.Send(command);

            return Ok(CustomAPIResponse<bool>.Success(res, StatusCodes.Status200OK));
        }

        [HttpPut("set-default/{id}")]
        public async Task<IActionResult> SetDefaultAddress([FromRoute] long id)
        {
            var command = new SetDefaultAddressCommand() { Id = id, UserId = _currentUserService.UserId };
            var res = await _sender.Send(command);

            return Ok(CustomAPIResponse<bool>.Success(res, StatusCodes.Status200OK));
        }

        [HttpGet("p")]
        [AllowAnonymous]
        public async Task<IActionResult> GetListProvince()
        {
            var query = new GetListProvinceQuery();
            var res = await _sender.Send(query);

            return Ok(CustomAPIResponse<List<ProvinceDto>>.Success(res, StatusCodes.Status200OK));
        }

        [HttpGet("p/{provinceId}/d")]
        [AllowAnonymous]
        public async Task<IActionResult> GetListDistrictByProvince([FromRoute] long provinceId)
        {
            var query = new GetListDistrictByProvinceQuery() { ProvinceId = provinceId };
            var res = await _sender.Send(query);

            return Ok(CustomAPIResponse<List<DistrictDto>>.Success(res, StatusCodes.Status200OK));
        }

        [HttpGet("p/d/{districtId}/w")]
        [AllowAnonymous]
        public async Task<IActionResult> GetListWardByDistrict([FromRoute] long districtId)
        {
            var query = new GetListWardByDistrictQuery() { DistrictId = districtId };
            var res = await _sender.Send(query);

            return Ok(CustomAPIResponse<List<WardDto>>.Success(res, StatusCodes.Status200OK));
        }


        //call from other service
        [HttpGet("internal")]
        [AllowAnonymous]
        public async Task<IActionResult> GetListAddressByUserFromOtherService([FromQuery] GetAddressPagingRequest request)
        {
            var query = _mapper.Map<GetListAddressQuery>(request);

            var res = await _sender.Send(query);

            return Ok(CustomAPIResponse<PaginatedResult<AddressDto>>.Success(res, StatusCodes.Status200OK));
        }
    }
}