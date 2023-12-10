using MediatR;
using SS_Microservice.Common.Types.Model.Paging;
using SS_Microservice.Services.Auth.Application.Dto;
using SS_Microservice.Services.Auth.Application.Interfaces;
using SS_Microservice.Services.Auth.Application.Model.User;

namespace SS_Microservice.Services.Auth.Application.Features.User.Queries
{
    public class GetListUserQuery : GetUserPagingRequest, IRequest<PaginatedResult<UserDto>>
    {
    }

    public class GetListUserHandler : IRequestHandler<GetListUserQuery, PaginatedResult<UserDto>>
    {
        private readonly IUserService _userService;

        public GetListUserHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<PaginatedResult<UserDto>> Handle(GetListUserQuery request, CancellationToken cancellationToken)
        {
            return await _userService.GetListUser(request);
        }
    }
}