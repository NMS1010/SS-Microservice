using MediatR;
using SS_Microservice.Services.Auth.Application.Interfaces;

namespace SS_Microservice.Services.Auth.Application.Features.User.Commands
{
    public class DisableListStaffCommand : IRequest<bool>
    {
        public List<long> StaffIds { get; set; }
    }

    public class DisableListStaffHandler : IRequestHandler<DisableListStaffCommand, bool>
    {
        private readonly IUserService _userService;

        public DisableListStaffHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<bool> Handle(DisableListStaffCommand request, CancellationToken cancellationToken)
        {
            return await _userService.DisableListStaffStatus(request);
        }
    }
}
