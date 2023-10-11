using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Services.Auth.Application.Dto;
using SS_Microservice.Services.Auth.Application.Features.User.Commands;
using SS_Microservice.Services.Auth.Application.Features.User.Queries;
using SS_Microservice.Services.Auth.Application.Model.User;

namespace SS_Microservice.Services.Auth.Application.Interfaces
{
    public interface IUserService
    {
        Task<string> CreateStaff(CreateStaffCommand command);

        Task<bool> UpdateUser(UpdateUserCommand command);

        Task<bool> UpdateStaff(UpdateStaffCommand command);

        Task<PaginatedResult<UserDto>> GetListUser(GetListUserQuery query);

        Task<PaginatedResult<StaffDto>> GetListStaff(GetListStaffQuery query);

        Task<UserDto> GetUser(GetUserQuery query);

        Task<StaffDto> GetStaff(GetStaffQuery query);

        Task<bool> ToggleUserStatus(ToggleUserCommand command);

        Task<bool> DisableListUserStatus(DisableListUserCommand command);

        Task<bool> ChangePassword(ChangePasswordCommand command);
    }
}