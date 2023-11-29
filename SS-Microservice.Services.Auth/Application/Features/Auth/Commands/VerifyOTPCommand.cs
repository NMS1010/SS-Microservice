using MediatR;
using SS_Microservice.Services.Auth.Application.Interfaces;
using SS_Microservice.Services.Auth.Application.Model.Auth;

namespace SS_Microservice.Services.Auth.Application.Features.Auth.Commands
{
    public class VerifyOTPCommand : VerifyOTPRequest, IRequest<bool>
    {
    }

    public class VerifyOTPHandler : IRequestHandler<VerifyOTPCommand, bool>
    {
        private readonly IAuthService _authService;

        public VerifyOTPHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<bool> Handle(VerifyOTPCommand request, CancellationToken cancellationToken)
        {
            var res = await _authService.VerifyOTP(request);
            return res;
        }
    }
}
