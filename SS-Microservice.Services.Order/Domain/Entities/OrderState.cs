using SS_Microservice.Common.Entities;

namespace SS_Microservice.Services.Order.Domain.Entities
{
    public class OrderState : BaseAuditableEntity<long>
    {
        public string OrderStateName { get; set; }
        public int Order { get; set; }
        public string HexColor { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedAt { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}