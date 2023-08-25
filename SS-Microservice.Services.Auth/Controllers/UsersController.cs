using AutoMapper;
using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SS_Microservice.Common.Services.CurrentUser;
using SS_Microservice.Services.Auth.Application.Auth.Queries;
using SS_Microservice.Services.Auth.Application.Dto;
using SS_Microservice.Services.Auth.Application.Model.Auth;
using SS_Microservice.Services.Auth.Application.Model.CustomResponse;
using SS_Microservice.Services.Auth.Application.Model.User;
using SS_Microservice.Services.Auth.Application.User.Commands;
using SS_Microservice.Services.Auth.Application.User.Queries;
using System.Security.Claims;

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

        [HttpGet("me")]
        public async Task<IActionResult> GetMe()
        {
            var res = await _mediator.Send(new GetUserQuery() { UserId = _currentUserService.UserId });
            return Ok(CustomAPIResponse<UserDto>.Success(res, StatusCodes.Status200OK));
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserById(string userId)
        {
            var res = await _mediator.Send(new GetUserQuery() { UserId = userId });
            return Ok(CustomAPIResponse<UserDto>.Success(res, StatusCodes.Status200OK));
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser(UserUpdateRequest request)
        {
            var userUpdateCommand = _mapper.Map<UserUpdateCommand>(request);
            await _mediator.Send(userUpdateCommand);
            return Ok(CustomAPIResponse<NoContentAPIResponse>.Success(StatusCodes.Status204NoContent));
        }
    }
}