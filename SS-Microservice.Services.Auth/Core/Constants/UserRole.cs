namespace SS_Microservice.Services.Auth.Core.Constants
{
    public class UserRole
    {
        public static string ADMIN = "ADMIN";
        public static string USER = "USER";

        public static List<string> Roles = new List<string>()
        {
            ADMIN,
            USER
        };
    }
}