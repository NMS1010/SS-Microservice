using SS_Microservice.Services.Auth.Application.Dto;
using SS_Microservice.Services.Auth.Application.Features.Auth.Commands;
using SS_Microservice.Services.Auth.Application.Features.Auth.Queries;

namespace SS_Microservice.Services.Auth.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthDto> Authenticate(LoginQuery query);

        Task<AuthDto> AuthenticateWithGoogle(GoogleAuthCommand command);

        Task<string> Register(RegisterUserCommand command, bool isGoogleAuthen = false);

        Task<AuthDto> RefreshToken(RefreshTokenCommand command);

        Task<bool> VerifyOTP(VerifyOTPCommand command);

        Task<bool> ResendOTP(ResendOTPCommand command);

        Task<bool> ForgotPassword(ForgotPasswordCommand command);

        Task<bool> ResetPassword(ResetPasswordCommand command);

        Task RevokeRefreshToken(RevokeRefreshTokenCommand command);

        Task RevokeAllRefreshToken();

    }
}