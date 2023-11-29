using MediatR;
using SS_Microservice.Services.Auth.Application.Interfaces;

namespace SS_Microservice.Services.Auth.Application.Features.Staff.Commands
{
    public class ToggleStaffCommand : IRequest<bool>
    {
        public long StaffId { get; set; }
    }

    public class ToggleStaffHandler : IRequestHandler<ToggleStaffCommand, bool>
    {
        private readonly IUserService _userService;

        public ToggleStaffHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<bool> Handle(ToggleStaffCommand request, CancellationToken cancellationToken)
        {
            return await _userService.ToggleStaffStatus(request);
        }
    }
}
