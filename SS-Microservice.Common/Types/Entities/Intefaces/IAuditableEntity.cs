namespace SS_Microservice.Common.Types.Entities.Intefaces
{
    public interface IAuditableEntity<T> : IEntity<T>
    {
        DateTime CreatedAt { get; set; }
        DateTime? UpdatedAt { get; set; }
        string CreatedBy { get; set; }
        string UpdatedBy { get; set; }
    }
}