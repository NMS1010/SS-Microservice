namespace SS_Microservice.Services.Auth.Core.Constants
{
    public class UserRole
    {
        public static List<string> UserRoles { get; set; } = new List<string>()
        {
            ADMIN,
            USER
        };

        public static string ADMIN = "ADMIN";
        public static string USER = "USER";
    }
}