using SS_Microservice.Common.Entities;

namespace SS_Microservice.Services.Order.Domain.Entities
{
    public class OrderCancellationReason : BaseAuditableEntity<long>
    {
        public string Name { get; set; }
        public string Note { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedAt { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}