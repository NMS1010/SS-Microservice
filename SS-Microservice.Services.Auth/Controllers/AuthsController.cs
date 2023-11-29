using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SS_Microservice.Common.Services.CurrentUser;
using SS_Microservice.Services.Auth.Application.Common.Constants;
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
        private readonly ISender _sender;
        private readonly ICurrentUserService _currentUserService;

        public AuthsController(ISender sender, IMapper mapper, ICurrentUserService currentUserService)
        {
            _sender = sender;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Authenticate([FromBody] LoginRequest request)
        {
            var token = await _sender.Send(_mapper.Map<LoginQuery>(request));
            return Ok(CustomAPIResponse<AuthDto>.Success(token, StatusCodes.Status200OK));
        }

        [HttpPost("google-login")]
        public async Task<IActionResult> AuthenticateWithGoogle([FromBody] GoogleAuthRequest request)
        {
            var authResp = await _sender.Send(_mapper.Map<GoogleAuthCommand>(request));

            return Ok(CustomAPIResponse<AuthDto>.Success(authResp, StatusCodes.Status200OK));
        }

        [HttpPut("register/verify")]
        public async Task<IActionResult> VerifyRegistation([FromBody] VerifyOTPRequest request)
        {
            request.Type = TOKEN_TYPE.REGISTER_OTP;
            var isSuccess = await _sender.Send(_mapper.Map<VerifyOTPCommand>(request));

            return Ok(CustomAPIResponse<bool>.Success(isSuccess, StatusCodes.Status204NoContent));
        }

        [HttpPut("register/resend")]
        public async Task<IActionResult> ResendRegistationOTP([FromBody] ResendOTPRequest request)
        {
            request.Type = TOKEN_TYPE.REGISTER_OTP;
            var isSuccess = await _sender.Send(_mapper.Map<ResendOTPCommand>(request));

            return Ok(CustomAPIResponse<bool>.Success(isSuccess, StatusCodes.Status204NoContent));
        }

        [HttpPut("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
        {
            var isSuccess = await _sender.Send(_mapper.Map<ForgotPasswordCommand>(request));

            return Ok(CustomAPIResponse<bool>.Success(isSuccess, StatusCodes.Status204NoContent));
        }

        [HttpPut("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            var isSuccess = await _sender.Send(_mapper.Map<ResetPasswordCommand>(request));

            return Ok(CustomAPIResponse<bool>.Success(isSuccess, StatusCodes.Status204NoContent));
        }

        [HttpPut("forgot-password/resend")]
        public async Task<IActionResult> ResendForgottenPasswordOTP([FromBody] ResendOTPRequest request)
        {
            request.Type = TOKEN_TYPE.FORGOT_PASSWORD_OTP;
            var isSuccess = await _sender.Send(_mapper.Map<ResendOTPCommand>(request));

            return Ok(CustomAPIResponse<bool>.Success(isSuccess, StatusCodes.Status204NoContent));
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var userId = await _sender.Send(_mapper.Map<RegisterUserCommand>(request));

            var url = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/api/users/{userId}";

            return Created(url, new CustomAPIResponse<object>(new { id = userId }, StatusCodes.Status201Created));
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            var newToken = await _sender.Send(_mapper.Map<RefreshTokenCommand>(request));

            return Ok(CustomAPIResponse<AuthDto>.Success(newToken, StatusCodes.Status200OK));
        }

        [HttpPost("revoke-refresh-token")]
        [Authorize]
        public async Task<IActionResult> RevokeRefreshToken()
        {
            await _sender.Send(new RevokeRefreshTokenCommand() { UserId = _currentUserService.UserId });

            return Ok(CustomAPIResponse<bool>.Success(true, StatusCodes.Status200OK));
        }
    }
}