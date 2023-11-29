namespace SS_Microservice.Common.Exceptions
{
	public class AccessDeniedException : Exception
	{
		public AccessDeniedException() : base("Not permission to access this resource")
		{ }

		public AccessDeniedException(string message) : base(message)
		{
		}
	}
}