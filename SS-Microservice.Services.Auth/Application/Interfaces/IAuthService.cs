using SS_Microservice.Services.Auth.Application.Dto;
using SS_Microservice.Services.Auth.Application.Features.Auth.Commands;
using SS_Microservice.Services.Auth.Application.Features.Auth.Queries;

namespace SS_Microservice.Services.Auth.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthDto> Authenticate(LoginQuery request);

        Task<AuthDto> RefreshToken(RefreshTokenCommand request);

        Task<bool> RevokeRefreshToken(string userId);

        Task RevokeAllRefreshToken();

        Task<string> Register(RegisterUserCommand request);
    }
}