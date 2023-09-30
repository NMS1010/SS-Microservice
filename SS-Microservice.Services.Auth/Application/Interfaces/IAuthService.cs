using SS_Microservice.Services.Auth.Application.Features.Auth.Commands;
using SS_Microservice.Services.Auth.Application.Features.Auth.Queries;
using SS_Microservice.Services.Auth.Application.Model.Auth;

namespace SS_Microservice.Services.Auth.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponse> Authenticate(LoginQuery request);

        Task<AuthResponse> RefreshToken(RefreshTokenCommand request);

        Task RevokeToken(string userId);

        Task RevokeAllToken();

        Task<string> Register(RegisterUserCommand request);
    }
}