using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SS_Microservice.Services.Order.Infrastructure.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Domain.Entities.Order>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Order> builder)
        {
            builder.HasIndex(u => u.Code).IsUnique();
            builder.Property(x => x.ShippingCost).HasColumnType("DECIMAL").IsRequired();
            builder.Property(x => x.TotalAmount).HasColumnType("DECIMAL").IsRequired();
            builder.Property(x => x.DeliveryMethod).IsRequired();
            builder.Property(x => x.PaymentStatus).IsRequired();
            builder.Property(x => x.Tax).IsRequired();
            builder.Property(x => x.Status).IsRequired();
        }
    }
}