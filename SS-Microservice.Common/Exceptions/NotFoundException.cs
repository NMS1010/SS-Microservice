namespace SS_Microservice.Common.Exceptions
{
    public class NotFoundException : Exception
    {
        private static string _message = "[RESOURCE NOT FOUND] ";
        public NotFoundException() : base(_message)
        {
        }

        public NotFoundException(string message)
            : base(_message + message)
        {
        }

        public NotFoundException(string name, object key)
            : base(_message + $"\"{name}\": ({key}) was not found.")
        {
        }
    }
}