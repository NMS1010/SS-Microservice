using SS_Microservice.Services.Auth.Application.Auth.Commands;
using SS_Microservice.Services.Auth.Application.Auth.Queries;
using SS_Microservice.Services.Auth.Application.Model.Auth;

namespace SS_Microservice.Services.Auth.Application.Common.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponse> Authenticate(LoginQuery request);

        Task<AuthResponse> RefreshToken(RefreshTokenCommand request);

        Task RevokeToken(string userId);

        Task RevokeAllToken();

        Task<bool> Register(RegisterCommand request);
    }
}