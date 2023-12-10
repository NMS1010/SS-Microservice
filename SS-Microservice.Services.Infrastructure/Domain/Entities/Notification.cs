using SS_Microservice.Common.Types.Entities;

namespace SS_Microservice.Services.Infrastructure.Domain.Entities
{
    public class Notification : BaseAuditableEntity<long>
    {
        public string UserId { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public string Anchor { get; set; }
        public bool Status { get; set; } = true;
    }
}