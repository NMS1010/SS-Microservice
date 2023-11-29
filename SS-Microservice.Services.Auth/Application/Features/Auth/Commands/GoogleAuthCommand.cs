using MediatR;
using SS_Microservice.Services.Auth.Application.Dto;
using SS_Microservice.Services.Auth.Application.Interfaces;
using SS_Microservice.Services.Auth.Application.Model.Auth;

namespace SS_Microservice.Services.Auth.Application.Features.Auth.Commands
{
    public class GoogleAuthCommand : GoogleAuthRequest, IRequest<AuthDto>
    {
    }

    public class GoogleAuthHandler : IRequestHandler<GoogleAuthCommand, AuthDto>
    {
        private readonly IAuthService _authService;

        public GoogleAuthHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<AuthDto> Handle(GoogleAuthCommand request, CancellationToken cancellationToken)
        {
            var res = await _authService.AuthenticateWithGoogle(request);
            return res;
        }
    }
}
