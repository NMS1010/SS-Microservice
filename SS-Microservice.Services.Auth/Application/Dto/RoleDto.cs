using SS_Microservice.Common.Types.Entities;

namespace SS_Microservice.Services.Auth.Application.Dto
{
    public class RoleDto : BaseAuditableEntity<string>
    {
        public string Name { get; set; }
    }
}
