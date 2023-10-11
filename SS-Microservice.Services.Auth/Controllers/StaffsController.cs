using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Common.Services.CurrentUser;
using SS_Microservice.Services.Auth.Application.Dto;
using SS_Microservice.Services.Auth.Application.Features.User.Commands;
using SS_Microservice.Services.Auth.Application.Features.User.Queries;
using SS_Microservice.Services.Auth.Application.Model.CustomResponse;
using SS_Microservice.Services.Auth.Application.Model.User;

namespace SS_Microservice.Services.Auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "ADMIN")]
    public class StaffsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ISender _mediator;

        public StaffsController(IMapper mapper, ISender mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateStaff([FromForm] CreateStaffRequest request)
        {
            var id = await _mediator.Send(_mapper.Map<CreateStaffCommand>(request));

            return Ok(CustomAPIResponse<object>.Success(new { id }, StatusCodes.Status201Created));
        }

        [HttpPut("{staffId}")]
        public async Task<IActionResult> UpdateStaff([FromRoute] long staffId, [FromForm] UpdateStaffRequest request)
        {
            request.Id = staffId;
            var res = await _mediator.Send(_mapper.Map<UpdateStaffCommand>(request));

            return Ok(CustomAPIResponse<bool>.Success(res, StatusCodes.Status200OK));
        }

        [HttpGet("{staffId}")]
        public async Task<IActionResult> GetStaff([FromRoute] long staffId)
        {
            var res = await _mediator.Send(new GetStaffQuery() { StaffId = staffId });

            return Ok(CustomAPIResponse<StaffDto>.Success(res, StatusCodes.Status200OK));
        }

        [HttpGet]
        public async Task<IActionResult> GetListStaff([FromQuery] GetUserPagingRequest request)
        {
            var res = await _mediator.Send(_mapper.Map<GetListStaffQuery>(request));

            return Ok(CustomAPIResponse<PaginatedResult<StaffDto>>.Success(res, StatusCodes.Status200OK));
        }
    }
}