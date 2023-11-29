using MediatR;
using SS_Microservice.Services.Auth.Application.Interfaces;
using SS_Microservice.Services.Auth.Application.Model.Auth;

namespace SS_Microservice.Services.Auth.Application.Features.Auth.Commands
{
    public class ForgotPasswordCommand : ForgotPasswordRequest, IRequest<bool>
    {
    }

    public class ForgotPasswordHandler : IRequestHandler<ForgotPasswordCommand, bool>
    {
        private readonly IAuthService _authService;

        public ForgotPasswordHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<bool> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var res = await _authService.ForgotPassword(request);
            return res;
        }
    }
}
