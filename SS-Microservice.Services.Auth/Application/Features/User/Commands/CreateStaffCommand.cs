using MediatR;
using SS_Microservice.Services.Auth.Application.Interfaces;
using SS_Microservice.Services.Auth.Application.Model.User;

namespace SS_Microservice.Services.Auth.Application.Features.User.Commands
{
    public class CreateStaffCommand : CreateStaffRequest, IRequest<string>
    {
    }

    public class CreateStaffHandler : IRequestHandler<CreateStaffCommand, string>
    {
        private readonly IUserService _userService;

        public CreateStaffHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<string> Handle(CreateStaffCommand request, CancellationToken cancellationToken)
        {
            return await _userService.CreateStaff(request);
        }
    }
}