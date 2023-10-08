using SS_Microservice.Common.Entities;

namespace SS_Microservice.Services.Order.Application.Dtos
{
    public class PaymentMethodDto : BaseAuditableEntity<long>
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public bool Status { get; set; }
        public string Image { get; set; }
    }
}