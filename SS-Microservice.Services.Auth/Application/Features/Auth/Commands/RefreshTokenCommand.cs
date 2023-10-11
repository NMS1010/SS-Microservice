using MediatR;
using SS_Microservice.Services.Auth.Application.Dto;
using SS_Microservice.Services.Auth.Application.Interfaces;
using SS_Microservice.Services.Auth.Application.Model.Auth;

namespace SS_Microservice.Services.Auth.Application.Features.Auth.Commands
{
    public class RefreshTokenCommand : RefreshTokenRequest, IRequest<AuthDto>
    {
    }

    public class RefreshTokenHandler : IRequestHandler<RefreshTokenCommand, AuthDto>
    {
        private readonly IAuthService _authService;

        public RefreshTokenHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<AuthDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var res = await _authService.RefreshToken(request);
            return res;
        }
    }
}