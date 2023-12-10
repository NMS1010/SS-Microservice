using SS_Microservice.Common.Types.Entities.Intefaces;

namespace SS_Microservice.Common.Types.Entities
{
    public class BaseEntity<T> : IEntity<T>
    {
        public T Id { get; set; }
    }
}