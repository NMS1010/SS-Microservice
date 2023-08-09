using MediatR;
using SS_Microservice.Services.Auth.Application.Common.Interfaces;
using SS_Microservice.Services.Auth.Application.Dto;
using SS_Microservice.Services.Auth.Application.User.Queries;

namespace SS_Microservice.Services.Auth.Application.User.Handlers
{
    public class GetUserHandler : IRequestHandler<GetUserQuery, UserDto>
    {
        private readonly IUserService _userService;

        public GetUserHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var res = await _userService.GetUser(request);
            return res;
        }
    }
}