namespace SS_Microservice.Services.Auth.Application.Model
{
    public class RefreshTokenRequest
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}