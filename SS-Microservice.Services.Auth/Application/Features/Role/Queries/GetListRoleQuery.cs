using MediatR;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Services.Auth.Application.Dto;
using SS_Microservice.Services.Auth.Application.Interfaces;
using SS_Microservice.Services.Auth.Application.Model.Role;

namespace SS_Microservice.Services.Auth.Application.Features.Role.Queries
{
    public class GetListRoleQuery : GetRolePagingRequest, IRequest<PaginatedResult<RoleDto>>
    {
    }

    public class GetListRoleHandler : IRequestHandler<GetListRoleQuery, PaginatedResult<RoleDto>>
    {
        private readonly IRoleService _roleService;

        public GetListRoleHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task<PaginatedResult<RoleDto>> Handle(GetListRoleQuery request, CancellationToken cancellationToken)
        {
            return await _roleService.GetListRole(request);
        }
    }
}
