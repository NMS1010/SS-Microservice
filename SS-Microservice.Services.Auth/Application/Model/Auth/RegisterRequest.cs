namespace SS_Microservice.Services.Auth.Application.Model.Auth
{
    public class RegisterRequest
    {
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}