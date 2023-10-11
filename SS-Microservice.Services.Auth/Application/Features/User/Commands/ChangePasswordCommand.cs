using MediatR;
using SS_Microservice.Services.Auth.Application.Interfaces;
using SS_Microservice.Services.Auth.Application.Model.User;

namespace SS_Microservice.Services.Auth.Application.Features.User.Commands
{
    public class ChangePasswordCommand : ChangePasswordRequest, IRequest<bool>
    {
    }

    public class ChangePasswordHandler : IRequestHandler<ChangePasswordCommand, bool>
    {
        private readonly IUserService _userService;

        public ChangePasswordHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<bool> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            return await _userService.ChangePassword(request);
        }
    }
}