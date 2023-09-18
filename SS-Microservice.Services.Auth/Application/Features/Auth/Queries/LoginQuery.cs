using MediatR;
using SS_Microservice.Services.Auth.Application.Model.Auth;

namespace SS_Microservice.Services.Auth.Application.Features.Auth.Queries
{
    public class LoginQuery : IRequest<AuthResponse>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}