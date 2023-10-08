using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SS_Microservice.Services.Auth.Domain.Entities;

namespace SS_Microservice.Services.Auth.Infrastructure.Data.Configuration
{
    public class AppUserTokenConfiguration : IEntityTypeConfiguration<AppUserTokens>
    {
        public void Configure(EntityTypeBuilder<AppUserTokens> builder)
        {
            builder.ToTable("AppUserToken");

            builder.Property(x => x.UserId)
                .IsRequired();

            builder.Property(x => x.Token)
                .IsRequired();

            builder.Property(x => x.Type)
                .IsRequired();

            builder.Property(x => x.ExpiredAt)
                .IsRequired(false);

            builder.HasOne(x => x.User)
                .WithMany(x => x.AppUserTokens)
                .HasForeignKey(x => x.UserId);
        }
    }
}