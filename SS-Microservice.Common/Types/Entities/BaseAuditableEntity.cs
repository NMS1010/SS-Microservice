using SS_Microservice.Common.Types.Entities.Intefaces;

namespace SS_Microservice.Common.Types.Entities
{
    public class BaseAuditableEntity<T> : BaseEntity<T>, IAuditableEntity<T>
    {
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}