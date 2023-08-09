namespace SS_Microservice.Services.Auth.Application.Model.Auth
{
    public class RegisterRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; } = DateTime.Now;
        public int Status { get; set; } = 1;
    }
}