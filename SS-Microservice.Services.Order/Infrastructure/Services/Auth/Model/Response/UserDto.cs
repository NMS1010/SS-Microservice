using SS_Microservice.Common.Types.Entities;

namespace SS_Microservice.Services.Order.Infrastructure.Services.Auth.Model.Response
{
    public class UserDto : BaseAuditableEntity<string>
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
