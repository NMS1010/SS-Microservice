namespace SS_Microservice.Services.Auth.Application.Dto
{
    public class CreateUserOTPDto
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string OTP { get; set; }
    }
}
