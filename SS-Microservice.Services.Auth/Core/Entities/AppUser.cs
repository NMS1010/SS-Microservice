using Microsoft.AspNetCore.Identity;

namespace SS_Microservice.Services.Auth.Core.Entities
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public int Status { get; set; }
        public string Avatar { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiredTime { get; set; }
    }
}