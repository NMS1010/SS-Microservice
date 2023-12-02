using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SS_Microservice.Services.UserOperation.Domain.Entities;

namespace SS_Microservice.Services.UserOperation.Infrastructure.Data.Configurations
{
    public class UserFollowProductConfiguration : IEntityTypeConfiguration<UserFollowProduct>
    {
        public void Configure(EntityTypeBuilder<UserFollowProduct> builder)
        {
            builder.Property(x => x.ProductId).IsRequired();
            builder.Property(x => x.UserId).IsRequired();
        }
    }
}
