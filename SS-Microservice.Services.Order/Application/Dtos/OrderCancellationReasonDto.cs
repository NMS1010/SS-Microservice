using SS_Microservice.Common.Entities;

namespace SS_Microservice.Services.Order.Application.Dtos
{
    public class OrderCancellationReasonDto : BaseAuditableEntity<long>
    {
        public string Name { get; set; }
        public string Note { get; set; }
        public bool Status { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}