using SS_Microservice.Common.Entities;

namespace SS_Microservice.Services.Order.Domain.Entities
{
    public class PaymentMethod : BaseAuditableEntity<long>
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public bool Status { get; set; } = true;
        public string Image { get; set; }
    }
}