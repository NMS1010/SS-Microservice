namespace SS_Microservice.Common.Exceptions
{
    public class InvalidRequestException : Exception
    {
        private static string _message = "[UNEXPECTED REQUEST DATA] ";
        public InvalidRequestException() : base(_message)
        { }

        public InvalidRequestException(string message) : base(_message + message)
        {
        }

        public InvalidRequestException(string message, Exception ex) : base(_message + message, ex)
        {
        }
    }
}