using MediatR;
using SS_Microservice.Services.Auth.Application.Model.Auth;

namespace SS_Microservice.Services.Auth.Application.Auth.Commands
{
    public class RefreshTokenCommand : IRequest<AuthResponse>
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}