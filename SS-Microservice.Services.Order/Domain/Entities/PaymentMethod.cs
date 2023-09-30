using SS_Microservice.Common.Entities;

namespace SS_Microservice.Services.Order.Domain.Entities
{
    public class PaymentMethod : BaseAuditableEntity<long>
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public int Status { get; set; }
        public string Image { get; set; }
    }
}