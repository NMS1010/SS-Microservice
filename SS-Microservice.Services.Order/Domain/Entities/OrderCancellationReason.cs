using SS_Microservice.Common.Types.Entities;

namespace SS_Microservice.Services.Order.Domain.Entities
{
    public class OrderCancellationReason : BaseAuditableEntity<long>
    {
        public string Name { get; set; }
        public string Note { get; set; }
        public bool Status { get; set; } = true;
        public ICollection<Order> Orders { get; set; }
    }
}