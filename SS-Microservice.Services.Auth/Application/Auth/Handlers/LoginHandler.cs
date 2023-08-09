using MediatR;
using SS_Microservice.Services.Auth.Application.Auth.Queries;
using SS_Microservice.Services.Auth.Application.Common.Interfaces;
using SS_Microservice.Services.Auth.Application.Model.Auth;

namespace SS_Microservice.Services.Auth.Application.Auth.Handlers
{
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