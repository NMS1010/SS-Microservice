using MediatR;
using SS_Microservice.Services.Auth.Application.Interfaces;

namespace SS_Microservice.Services.Auth.Application.Features.Auth.Commands
{
    public class RevokeTokenCommand : IRequest<bool>
    {
        public string UserId { get; set; }
    }

    public class RevokeTokenHandler : IRequestHandler<RevokeTokenCommand, bool>
    {
        private readonly IAuthService _authService;

        public RevokeTokenHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<bool> Handle(RevokeTokenCommand request, CancellationToken cancellationToken)
        {
            return await _authService.RevokeRefreshToken(request.UserId);
        }
    }
}