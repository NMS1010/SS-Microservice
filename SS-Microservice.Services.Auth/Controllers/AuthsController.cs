using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SS_Microservice.Common.Services.CurrentUser;
using SS_Microservice.Services.Auth.Application.Dto;
using SS_Microservice.Services.Auth.Application.Features.Auth.Commands;
using SS_Microservice.Services.Auth.Application.Features.Auth.Queries;
using SS_Microservice.Services.Auth.Application.Model.Auth;
using SS_Microservice.Services.Auth.Application.Model.CustomResponse;

namespace SS_Microservice.Services.Auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ISender _mediator;
        private readonly ICurrentUserService _currentUserService;

        public AuthsController(ISender mediator, IMapper mapper, ICurrentUserService currentUserService)
        {
            _mediator = mediator;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Authenticate([FromBody] LoginRequest request)
        {
            var token = await _mediator.Send(_mapper.Map<LoginQuery>(request));
            return Ok(CustomAPIResponse<AuthDto>.Success(token, StatusCodes.Status200OK));
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var success = await _mediator.Send(_mapper.Map<RegisterUserCommand>(request));

            return Ok(CustomAPIResponse<bool>.Success(success, StatusCodes.Status201Created));
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            var newToken = await _mediator.Send(_mapper.Map<RefreshTokenCommand>(request));

            return Ok(CustomAPIResponse<AuthDto>.Success(newToken, StatusCodes.Status200OK));
        }

        [HttpPost("revoke-refresh-token")]
        [Authorize]
        public async Task<IActionResult> RevokeRefreshToken()
        {
            var isSuccess = await _mediator.Send(new RevokeTokenCommand() { UserId = _currentUserService.UserId });

            return Ok(CustomAPIResponse<bool>.Success(isSuccess, StatusCodes.Status200OK));
        }
    }
}