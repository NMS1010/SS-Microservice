using MediatR;
using SS_Microservice.Services.Auth.Application.Dto;
using SS_Microservice.Services.Auth.Application.Interfaces;

namespace SS_Microservice.Services.Auth.Application.Features.User.Queries
{
    public class GetStaffQuery : IRequest<StaffDto>
    {
        public long StaffId { get; set; }
    }

    public class GetStaffHandler : IRequestHandler<GetStaffQuery, StaffDto>
    {
        private readonly IUserService _userService;

        public GetStaffHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<StaffDto> Handle(GetStaffQuery request, CancellationToken cancellationToken)
        {
            return await _userService.GetStaff(request);
        }
    }
}