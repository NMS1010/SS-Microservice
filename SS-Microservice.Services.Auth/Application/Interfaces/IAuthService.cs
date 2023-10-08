using SS_Microservice.Services.Auth.Application.Features.Auth.Commands;
using SS_Microservice.Services.Auth.Application.Features.Auth.Queries;
using SS_Microservice.Services.Auth.Application.Model.Auth;

namespace SS_Microservice.Services.Auth.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponse> Authenticate(LoginQuery request);

        Task<AuthResponse> RefreshToken(RefreshTokenCommand request);

        Task RevokeRefreshToken(string userId);

        Task RevokeAllRefreshToken();

        Task<string> Register(RegisterUserCommand request);
    }
}