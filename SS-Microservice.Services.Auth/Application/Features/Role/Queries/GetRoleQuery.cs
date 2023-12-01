using MediatR;
using SS_Microservice.Services.Auth.Application.Dto;
using SS_Microservice.Services.Auth.Application.Interfaces;

namespace SS_Microservice.Services.Auth.Application.Features.Role.Queries
{
    public class GetRoleQuery : IRequest<RoleDto>
    {
        public string Id { get; set; }
    }

    public class GetRoleHandler : IRequestHandler<GetRoleQuery, RoleDto>
    {
        private readonly IRoleService _roleService;

        public GetRoleHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task<RoleDto> Handle(GetRoleQuery request, CancellationToken cancellationToken)
        {
            return await _roleService.GetRole(request);
        }
    }
}
