using MediatR;
using SS_Microservice.Services.Auth.Application.Dto;
using SS_Microservice.Services.Auth.Application.Interfaces;
using SS_Microservice.Services.Auth.Application.Model.Auth;

namespace SS_Microservice.Services.Auth.Application.Features.Auth.Queries
{
    public class LoginQuery : LoginRequest, IRequest<AuthDto>
    {
    }

    public class LoginHandler : IRequestHandler<LoginQuery, AuthDto>
    {
        private readonly IAuthService _authService;

        public LoginHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<AuthDto> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var res = await _authService.Authenticate(request);

            return res;
        }
    }
}