using SS_Microservice.Common.Types.Entities;

namespace SS_Microservice.Services.UserOperation.Domain.Entities
{
    public class UserFollowProduct : BaseAuditableEntity<long>
    {
        public string UserId { get; set; }
        public long ProductId { get; set; }
    }
}