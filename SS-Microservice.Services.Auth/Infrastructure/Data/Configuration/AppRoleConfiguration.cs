using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SS_Microservice.Services.Auth.Application.Common.Constants;
using SS_Microservice.Services.Auth.Domain.Entities;

namespace SS_Microservice.Services.Auth.Infrastructure.Data.Configuration
{
    public class AppRoleConfiguration : IEntityTypeConfiguration<AppRole>
    {
        public void Configure(EntityTypeBuilder<AppRole> builder)
        {
            var now = DateTime.Now;
            builder.HasData(USER_ROLE.Roles.Select(x => new AppRole()
            {
                Name = x,
                CreatedAt = now,
                CreatedBy = "System",
                NormalizedName = x
            }));
        }
    }
}