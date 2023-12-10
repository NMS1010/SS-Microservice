using SS_Microservice.Common.Types.Entities;

namespace SS_Microservice.Services.UserOperation.Domain.Entities
{
    public class Review : BaseAuditableEntity<long>
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public int Rating { get; set; }
        public string Image { get; set; }
        public string Reply { get; set; }
        public bool Status { get; set; } = true;
        public long ProductId { get; set; }
        public string UserId { get; set; }
        public long OrderItemId { get; set; }
    }
}