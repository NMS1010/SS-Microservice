using AutoMapper;
using Consul;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SS_Microservice.Services.Auth.Application.Auth.Commands;
using SS_Microservice.Services.Auth.Application.Auth.Queries;
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

        public AuthsController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Authenticate([FromBody] LoginRequest request)
        {
            var token = await _mediator.Send(_mapper.Map<LoginQuery>(request));
            return Ok(CustomAPIResponse<AuthResponse>.Success(token, StatusCodes.Status200OK));
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
            return Ok(CustomAPIResponse<AuthResponse>.Success(newToken, StatusCodes.Status200OK));
        }
    }
}