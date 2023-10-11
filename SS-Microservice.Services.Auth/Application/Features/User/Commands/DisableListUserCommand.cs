using MediatR;
using SS_Microservice.Services.Auth.Application.Interfaces;

namespace SS_Microservice.Services.Auth.Application.Features.User.Commands
{
    public class DisableListUserCommand : IRequest<bool>
    {
        public List<string> UserIds { get; set; }
    }

    public class DisableListUserHandler : IRequestHandler<DisableListUserCommand, bool>
    {
        private readonly IUserService _userService;

        public DisableListUserHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<bool> Handle(DisableListUserCommand request, CancellationToken cancellationToken)
        {
            return await _userService.DisableListUserStatus(request);
        }
    }
}