using MediatR;
using SS_Microservice.Services.Auth.Application.Interfaces;
using SS_Microservice.Services.Auth.Application.Model.Auth;

namespace SS_Microservice.Services.Auth.Application.Features.Auth.Queries
{
    public class LoginQuery : IRequest<AuthResponse>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class LoginHandler : IRequestHandler<LoginQuery, AuthResponse>
    {
        private readonly IAuthService _authService;

        public LoginHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<AuthResponse> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var res = await _authService.Authenticate(request);

            return res;
        }
    }
}