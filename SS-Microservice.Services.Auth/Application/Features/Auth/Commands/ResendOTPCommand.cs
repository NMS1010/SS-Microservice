using MediatR;
using SS_Microservice.Services.Auth.Application.Interfaces;
using SS_Microservice.Services.Auth.Application.Model.Auth;

namespace SS_Microservice.Services.Auth.Application.Features.Auth.Commands
{
    public class ResendOTPCommand : ResendOTPRequest, IRequest<bool>
    {
    }

    public class ResendOTPHandler : IRequestHandler<ResendOTPCommand, bool>
    {
        private readonly IAuthService _authService;

        public ResendOTPHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<bool> Handle(ResendOTPCommand request, CancellationToken cancellationToken)
        {
            var res = await _authService.ResendOTP(request);
            return res;
        }
    }
}
