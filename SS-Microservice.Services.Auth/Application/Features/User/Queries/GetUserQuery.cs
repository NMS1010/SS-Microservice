using MediatR;
using SS_Microservice.Services.Auth.Application.Dto;
using SS_Microservice.Services.Auth.Application.Interfaces;

namespace SS_Microservice.Services.Auth.Application.Features.User.Queries
{
    public class GetUserQuery : IRequest<UserDto>
    {
        public string UserId { get; set; }
    }

    public class GetUserHandler : IRequestHandler<GetUserQuery, UserDto>
    {
        private readonly IUserService _userService;

        public GetUserHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            return await _userService.GetUser(request);
        }
    }
}