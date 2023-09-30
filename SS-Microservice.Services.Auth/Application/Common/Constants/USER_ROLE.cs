namespace SS_Microservice.Services.Auth.Application.Common.Constants
{
    public class USER_ROLE
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