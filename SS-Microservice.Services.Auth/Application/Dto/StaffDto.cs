using SS_Microservice.Common.Entities;

namespace SS_Microservice.Services.Auth.Application.Dto
{
    public class StaffDto : BaseAuditableEntity<long>
    {
        public string Type { get; set; }
        public string Code { get; set; }
        public UserDto User { get; set; }
        public string UserId { get; set; }
    }
}