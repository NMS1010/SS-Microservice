using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SS_Microservice.Services.Order.Domain.Entities;

namespace SS_Microservice.Services.Order.Infrastructure.Data.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.Property(x => x.UnitPrice).HasColumnType("DECIMAL").IsRequired();
            builder.Property(x => x.TotalPrice).HasColumnType("DECIMAL").IsRequired();
            builder.Property(x => x.Quantity).IsRequired();
        }
    }
}