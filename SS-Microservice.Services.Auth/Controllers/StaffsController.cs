using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SS_Microservice.Common.Types.Model.CustomResponse;
using SS_Microservice.Common.Types.Model.Paging;
using SS_Microservice.Services.Auth.Application.Dto;
using SS_Microservice.Services.Auth.Application.Features.Staff.Commands;
using SS_Microservice.Services.Auth.Application.Features.User.Commands;
using SS_Microservice.Services.Auth.Application.Features.User.Queries;
using SS_Microservice.Services.Auth.Application.Model.User;

namespace SS_Microservice.Services.Auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "ADMIN")]
    public class StaffsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ISender _sender;

        public StaffsController(IMapper mapper, ISender sender)
        {
            _mapper = mapper;
            _sender = sender;
        }

        [HttpPost]
        public async Task<IActionResult> CreateStaff([FromBody] CreateStaffRequest request)
        {
            var id = await _sender.Send(_mapper.Map<CreateStaffCommand>(request));

            return Ok(CustomAPIResponse<object>.Success(new { id }, StatusCodes.Status201Created));
        }

        [HttpPut("{staffId}")]
        public async Task<IActionResult> UpdateStaff([FromRoute] long staffId, [FromBody] UpdateStaffRequest request)
        {
            request.Id = staffId;
            var res = await _sender.Send(_mapper.Map<UpdateStaffCommand>(request));

            return Ok(CustomAPIResponse<bool>.Success(res, StatusCodes.Status200OK));
        }

        [HttpGet("{staffId}")]
        public async Task<IActionResult> GetStaff([FromRoute] long staffId)
        {
            var res = await _sender.Send(new GetStaffQuery() { StaffId = staffId });

            return Ok(CustomAPIResponse<StaffDto>.Success(res, StatusCodes.Status200OK));
        }

        [HttpGet]
        public async Task<IActionResult> GetListStaff([FromQuery] GetUserPagingRequest request)
        {
            var res = await _sender.Send(_mapper.Map<GetListStaffQuery>(request));

            return Ok(CustomAPIResponse<PaginatedResult<StaffDto>>.Success(res, StatusCodes.Status200OK));
        }

        [HttpDelete("{staffId}")]
        public async Task<IActionResult> ToggleStaffStatus([FromRoute] long staffId)
        {
            var res = await _sender.Send(new ToggleStaffCommand() { StaffId = staffId });

            return Ok(CustomAPIResponse<bool>.Success(res, StatusCodes.Status200OK));
        }

        [HttpDelete]
        public async Task<IActionResult> DisableListStaffStatus([FromQuery] List<long> ids)
        {
            var res = await _sender.Send(new DisableListStaffCommand() { StaffIds = ids });

            return Ok(CustomAPIResponse<bool>.Success(res, StatusCodes.Status200OK));
        }
    }
}