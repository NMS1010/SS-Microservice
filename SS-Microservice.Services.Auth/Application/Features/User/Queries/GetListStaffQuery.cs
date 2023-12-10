using MediatR;
using SS_Microservice.Common.Types.Model.Paging;
using SS_Microservice.Services.Auth.Application.Dto;
using SS_Microservice.Services.Auth.Application.Interfaces;
using SS_Microservice.Services.Auth.Application.Model.User;

namespace SS_Microservice.Services.Auth.Application.Features.User.Queries
{
    public class GetListStaffQuery : GetUserPagingRequest, IRequest<PaginatedResult<StaffDto>>
    {
    }

    public class GetListStaffHandler : IRequestHandler<GetListStaffQuery, PaginatedResult<StaffDto>>
    {
        private readonly IUserService _userService;

        public GetListStaffHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<PaginatedResult<StaffDto>> Handle(GetListStaffQuery request, CancellationToken cancellationToken)
        {
            return await _userService.GetListStaff(request);
        }
    }
}