using SS_Microservice.Common.Types.Entities;

namespace SS_Microservice.Services.Auth.Domain.Entities
{
    public class Staff : BaseAuditableEntity<long>
    {
        public string Type { get; set; }
        public string Code { get; set; }
        public AppUser User { get; set; }
        public string UserId { get; set; }
    }
}