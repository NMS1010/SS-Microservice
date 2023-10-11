using Microsoft.AspNetCore.Identity;
using SS_Microservice.Common.Entities.Intefaces;

namespace SS_Microservice.Services.Auth.Domain.Entities
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? Dob { get; set; }
        public string Gender { get; set; }
        public string Avatar { get; set; }
        public int Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public Staff Staff { get; set; }
        public ICollection<AppUserToken> AppUserTokens { get; set; } = new List<AppUserToken>();
    }
}