using MediatR;
using SS_Microservice.Services.Auth.Application.Auth.Commands;
using SS_Microservice.Services.Auth.Application.Common.Interfaces;

namespace SS_Microservice.Services.Auth.Application.Auth.Handlers
{
    public class RegisterHandler : IRequestHandler<RegisterCommand, bool>
    {
        private readonly IAuthService _authService;

        public RegisterHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<bool> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var res = await _authService.Register(request);
            return res;
        }
    }
}