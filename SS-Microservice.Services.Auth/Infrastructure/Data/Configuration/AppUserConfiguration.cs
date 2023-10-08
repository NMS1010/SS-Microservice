using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SS_Microservice.Services.Auth.Domain.Entities;

namespace SS_Microservice.Services.Auth.Infrastructure.Data.Configuration
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.ToTable("User");

            builder.Property(x => x.FirstName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.LastName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Status)
                .IsRequired();

            builder.HasMany(x => x.AppUserTokens)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);
        }
    }
}