namespace SS_Microservice.Services.Auth.Application.Interfaces
{
    public interface ITokenService
    {
        public string GenerateOTP(int digitNumber = 6);
    }
}