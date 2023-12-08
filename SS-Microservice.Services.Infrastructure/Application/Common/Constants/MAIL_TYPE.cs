namespace SS_Microservice.Services.Infrastructure.Application.Common.Constants
{
    public static class MAIL_TYPE
    {
        public const string FORGOT_PASSWORD = "forgot-password.html";
        public const string REGISTATION = "confirm-registation.html";
        public const string RESEND = "resend.html";
        public const string ORDER_CONFIRMATION = "order-confirmation.html";

        public static Dictionary<string, string> Subject = new()
        {
            { FORGOT_PASSWORD, "Quên mật khẩu" },
            { REGISTATION, "Xác nhận đăng ký" },
            { RESEND, "Gửi lại mã xác nhận" },
            { ORDER_CONFIRMATION, "Xác nhận đơn hàng" }
        };
    }
}