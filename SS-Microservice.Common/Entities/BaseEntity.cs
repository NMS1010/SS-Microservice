using SS_Microservice.Common.Entities.Intefaces;

namespace SS_Microservice.Common.Entities
{
	public class BaseEntity<T> : IEntity<T>
	{
		public T Id { get; set; }
	}
}