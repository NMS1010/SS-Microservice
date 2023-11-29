namespace SS_Microservice.Services.Auth.Application.Common.Constants
{
    public class TOKEN_TYPE
    {
        public static string TOKEN = "TOKEN";
        public static string REFRESH_TOKEN = "REFRESH_TOKEN";
        public static string REGISTER_OTP = "REGISTER_OTP";
        public static string FORGOT_PASSWORD_OTP = "FORGOT_PASSWORD_OTP";

        public static int OTP_EXPIRY_MINUTES = 5;
        public static int REFRESH_TOKEN_EXPIRY_DAYS = 7;
    }
}