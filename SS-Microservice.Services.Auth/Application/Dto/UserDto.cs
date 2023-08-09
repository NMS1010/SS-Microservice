namespace SS_Microservice.Services.Auth.Application.Dto
{
    public class UserDto
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Username { get; set; }
        public int Status { get; set; } = 1;
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}