using System.Security.Claims;

namespace SS_Microservice.Services.Auth.Application.Common.Interfaces
{
    public interface IJwtService
    {
        Task<string> CreateJWT(string userId);

        ClaimsPrincipal ValidateExpiredJWT(string token);

        string CreateRefreshToken();
    }
}