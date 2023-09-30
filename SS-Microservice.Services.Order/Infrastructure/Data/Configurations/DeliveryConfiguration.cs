using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SS_Microservice.Services.Order.Domain.Entities;

namespace SS_Microservice.Services.Order.Infrastructure.Data.Configurations
{
    public class DeliveryConfiguration : IEntityTypeConfiguration<Delivery>
    {
        public void Configure(EntityTypeBuilder<Delivery> builder)
        {
            builder.ToTable("delivery");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.Image).IsRequired();
            builder.Property(x => x.Description).IsRequired(false);
            builder.Property(x => x.Price)
                .HasColumnType("DECIMAL")
                .IsRequired();
        }
    }
}