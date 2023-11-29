namespace SS_Microservice.Common.Exceptions
{
	public class NotFoundException : Exception
	{
		public NotFoundException()
		{
		}

		public NotFoundException(string message)
			: base(message)
		{
		}

		public NotFoundException(string name, object key)
			: base($"\"{name}\": ({key}) was not found.")
		{
		}
	}
}