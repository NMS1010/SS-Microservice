using Microsoft.AspNetCore.Identity;

namespace SS_Microservice.Services.Auth.Domain.Entities
{
    public class AppRole : IdentityRole
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}