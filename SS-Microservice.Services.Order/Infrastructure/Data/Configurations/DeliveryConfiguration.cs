using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SS_Microservice.Services.Order.Domain.Entities;

namespace SS_Microservice.Services.Order.Infrastructure.Data.Configurations
{
    public class DeliveryConfiguration : IEntityTypeConfiguration<Delivery>
    {
        public void Configure(EntityTypeBuilder<Delivery> builder)
        {
            builder.HasIndex(u => u.Name).IsUnique();
            builder.Property(x => x.Price).HasColumnType("DECIMAL");
            builder.Property(x => x.Image).IsRequired();
            builder.Property(x => x.Status).IsRequired();
        }
    }
}