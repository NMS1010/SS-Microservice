namespace SS_Microservice.Common.Exceptions
{
    public class UnAuthorizedException : Exception
    {
        private static string _message = "[UNAUTHORIZED] ";
        public UnAuthorizedException() : base(_message)
        {
        }

        public UnAuthorizedException(string message)
            : base(_message + message)
        {
        }

        public UnAuthorizedException(string message, Exception ex)
            : base(_message + message, ex)
        {
        }
    }
}