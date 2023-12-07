namespace SS_Microservice.Common.Exceptions
{
    public class InternalServiceCommunicationException : Exception
    {
        private static string _message = "[INTERNAL SERVICE COMMUNICATION ERROR] ";
        public InternalServiceCommunicationException() : base(_message)
        { }

        public InternalServiceCommunicationException(string message) : base(_message + message)
        {
        }

        public InternalServiceCommunicationException(string message, Exception ex) : base(_message + message, ex)
        {
        }
    }
}
