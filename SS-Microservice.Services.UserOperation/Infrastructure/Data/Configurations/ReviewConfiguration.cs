using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SS_Microservice.Services.UserOperation.Domain.Entities;

namespace SS_Microservice.Services.UserOperation.Infrastructure.Data.Configurations
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.Property(x => x.Title).IsRequired();
            builder.Property(x => x.Content).IsRequired();
            builder.Property(x => x.Rating).IsRequired();
            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.ProductId).IsRequired();
            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.OrderItemId).IsRequired();
        }
    }
}