namespace SS_Microservice.Common.Exceptions
{
	public class UnAuthorizedException : Exception
	{
		public UnAuthorizedException()
		{
		}

		public UnAuthorizedException(string message)
			: base(message)
		{
		}

		public UnAuthorizedException(string message, Exception ex)
			: base(message, ex)
		{
		}
	}
}