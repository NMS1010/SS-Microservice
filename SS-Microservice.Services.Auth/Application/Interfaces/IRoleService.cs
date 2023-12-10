using SS_Microservice.Common.Types.Model.Paging;
using SS_Microservice.Services.Auth.Application.Dto;
using SS_Microservice.Services.Auth.Application.Features.Role.Queries;

namespace SS_Microservice.Services.Auth.Application.Interfaces
{
    public interface IRoleService
    {
        Task<PaginatedResult<RoleDto>> GetListRole(GetListRoleQuery query);

        Task<RoleDto> GetRole(GetRoleQuery query);
    }
}
