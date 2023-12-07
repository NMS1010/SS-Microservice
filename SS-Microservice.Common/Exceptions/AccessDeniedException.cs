namespace SS_Microservice.Common.Exceptions
{
    public class AccessDeniedException : Exception
    {
        private static string _message = "[ACCESS DENIED] ";
        public AccessDeniedException() : base(_message + "Not permission to access this resource")
        { }

        public AccessDeniedException(string message) : base(_message + message)
        {
        }
    }
}