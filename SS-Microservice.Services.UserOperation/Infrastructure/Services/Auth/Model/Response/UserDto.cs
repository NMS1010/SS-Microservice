namespace SS_Microservice.Services.UserOperation.Infrastructure.Services.Auth.Model.Response
{
    public class UserDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime? Dob { get; set; }
        public string Gender { get; set; }
        public string Avatar { get; set; }
        public int Status { get; set; }

        public List<string> Roles { get; set; } = new List<string>();
    }
}
