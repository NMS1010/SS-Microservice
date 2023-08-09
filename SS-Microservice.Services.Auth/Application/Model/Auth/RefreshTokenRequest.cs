namespace SS_Microservice.Services.Auth.Application.Model.Auth
{
    public class RefreshTokenRequest
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}