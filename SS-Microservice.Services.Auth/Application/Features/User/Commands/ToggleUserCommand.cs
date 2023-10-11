using MediatR;
using SS_Microservice.Services.Auth.Application.Interfaces;

namespace SS_Microservice.Services.Auth.Application.Features.User.Commands
{
    public class ToggleUserCommand : IRequest<bool>
    {
        public string UserId { get; set; }
    }

    public class ToggleUserHandler : IRequestHandler<ToggleUserCommand, bool>
    {
        private readonly IUserService _userService;

        public ToggleUserHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<bool> Handle(ToggleUserCommand request, CancellationToken cancellationToken)
        {
            return await _userService.ToggleUserStatus(request);
        }
    }
}