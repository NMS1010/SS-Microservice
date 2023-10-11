using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SS_Microservice.Services.Auth.Domain.Entities;
using SS_Microservice.Services.Auth.Infrastructure.Data.Configuration;
using System.Reflection.Emit;

namespace SS_Microservice.Services.Auth.Infrastructure.Data.DBContext
{
    public class AuthDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public AuthDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            foreach (var type in builder.Model.GetEntityTypes())
            {
                string tableName = type.GetTableName();
                if (tableName.StartsWith("AspNet"))
                {
                    type.SetTableName(tableName.Substring(6));
                }
            }
            builder.ApplyConfigurationsFromAssembly(typeof(AppUserConfiguration).Assembly);
        }

        public DbSet<AppUserToken> AppUserTokens { get; set; }
        public DbSet<Staff> Staffs { get; set; }
    }
}