using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Services.Auth.Application.Dto;
using SS_Microservice.Services.Auth.Application.Features.Role.Queries;
using SS_Microservice.Services.Auth.Application.Model.CustomResponse;
using SS_Microservice.Services.Auth.Application.Model.Role;

namespace SS_Microservice.Services.Auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "ADMIN")]
    public class RolesController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public RolesController(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetListRole([FromQuery] GetRolePagingRequest request)
        {
            var roles = await _sender.Send(_mapper.Map<GetListRoleQuery>(request));

            return Ok(CustomAPIResponse<PaginatedResult<RoleDto>>.Success(roles, StatusCodes.Status200OK));
        }

        [HttpGet("{roleId}")]
        public async Task<IActionResult> GetRole([FromRoute] string roleId)
        {
            var role = await _sender.Send(new GetRoleQuery()
            {
                Id = roleId
            });

            return Ok(CustomAPIResponse<RoleDto>.Success(role, StatusCodes.Status200OK));
        }
    }
}