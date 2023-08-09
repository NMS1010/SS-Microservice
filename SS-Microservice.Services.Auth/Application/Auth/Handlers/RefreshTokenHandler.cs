using MediatR;
using SS_Microservice.Services.Auth.Application.Auth.Commands;
using SS_Microservice.Services.Auth.Application.Common.Interfaces;
using SS_Microservice.Services.Auth.Application.Model.Auth;

namespace SS_Microservice.Services.Auth.Application.Auth.Handlers
{
    public class RefreshTokenHandler : IRequestHandler<RefreshTokenCommand, AuthResponse>
    {
        private readonly IAuthService _authService;

        public RefreshTokenHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<AuthResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var res = await _authService.RefreshToken(request);
            return res;
        }
    }
}