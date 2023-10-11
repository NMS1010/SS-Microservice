﻿using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ISender _mediator;
        private readonly ICurrentUserService _currentUserService;

        public UsersController(IMapper mapper, ISender mediator, ICurrentUserService currentUserService)
        {
            _mapper = mapper;
            _mediator = mediator;
            _currentUserService = currentUserService;
        }

        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            request.UserId = _currentUserService.UserId;
            var res = await _mediator.Send(_mapper.Map<ChangePasswordCommand>(request));

            return Ok(CustomAPIResponse<bool>.Success(res, StatusCodes.Status200OK));
        }

        [HttpPut("toggle/{userId}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> ToggleUserStatus([FromRoute] string userId)
        {
            var res = await _mediator.Send(new ToggleUserCommand() { UserId = userId });

            return Ok(CustomAPIResponse<bool>.Success(res, StatusCodes.Status200OK));
        }

        [HttpPut("disable-list")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> DisableListUsersStatus([FromQuery] List<string> userIds)
        {
            var res = await _mediator.Send(new DisableListUserCommand() { UserIds = userIds });

            return Ok(CustomAPIResponse<bool>.Success(res, StatusCodes.Status200OK));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromForm] UpdateUserRequest request)
        {
            request.UserId = _currentUserService.UserId;
            var res = await _mediator.Send(_mapper.Map<UpdateUserCommand>(request));

            return Ok(CustomAPIResponse<bool>.Success(res, StatusCodes.Status200OK));
        }

        [HttpGet("{userId}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> GetUser([FromRoute] string userId)
        {
            var res = await _mediator.Send(new GetUserQuery() { UserId = userId });

            return Ok(CustomAPIResponse<UserDto>.Success(res, StatusCodes.Status200OK));
        }

        [HttpGet("profile/me")]
        public async Task<IActionResult> GetMe()
        {
            var res = await _mediator.Send(new GetUserQuery() { UserId = _currentUserService.UserId });

            return Ok(CustomAPIResponse<UserDto>.Success(res, StatusCodes.Status200OK));
        }

        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> GetListUser([FromQuery] GetUserPagingRequest request)
        {
            var res = await _mediator.Send(_mapper.Map<GetListUserQuery>(request));

            return Ok(CustomAPIResponse<PaginatedResult<UserDto>>.Success(res, StatusCodes.Status200OK));
        }
    }
}