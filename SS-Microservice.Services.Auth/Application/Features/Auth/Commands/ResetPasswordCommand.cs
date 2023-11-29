using MediatR;
using SS_Microservice.Services.Auth.Application.Interfaces;
using SS_Microservice.Services.Auth.Application.Model.Auth;

namespace SS_Microservice.Services.Auth.Application.Features.Auth.Commands
{
    public class ResetPasswordCommand : ResetPasswordRequest, IRequest<bool>
    {
    }

    public class ResetPasswordHandler : IRequestHandler<ResetPasswordCommand, bool>
    {
        private readonly IAuthService _authService;

        public ResetPasswordHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<bool> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var res = await _authService.ResetPassword(request);
            return res;
        }
    }
}
