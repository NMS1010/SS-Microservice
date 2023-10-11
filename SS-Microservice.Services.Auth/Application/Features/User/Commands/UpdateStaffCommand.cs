using MediatR;
using SS_Microservice.Services.Auth.Application.Interfaces;
using SS_Microservice.Services.Auth.Application.Model.User;

namespace SS_Microservice.Services.Auth.Application.Features.User.Commands
{
    public class UpdateStaffCommand : UpdateStaffRequest, IRequest<bool>
    {
    }

    public class UpdateStaffHandler : IRequestHandler<UpdateStaffCommand, bool>
    {
        private readonly IUserService _userService;

        public UpdateStaffHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<bool> Handle(UpdateStaffCommand request, CancellationToken cancellationToken)
        {
            return await _userService.UpdateStaff(request);
        }
    }
}