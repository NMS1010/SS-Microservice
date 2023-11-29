using MediatR;
using SS_Microservice.Services.Auth.Application.Interfaces;

namespace SS_Microservice.Services.Auth.Application.Features.Auth.Commands
{
    public class RevokeRefreshTokenCommand : IRequest
    {
        public string UserId { get; set; }
    }

    public class RevokeTokenHandler : IRequestHandler<RevokeRefreshTokenCommand>
    {
        private readonly IAuthService _authService;

        public RevokeTokenHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task Handle(RevokeRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            await _authService.RevokeRefreshToken(request);
        }
    }
}