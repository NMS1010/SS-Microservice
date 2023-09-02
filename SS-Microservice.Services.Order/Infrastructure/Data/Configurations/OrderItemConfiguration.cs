using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SS_Microservice.Services.Order.Core.Entities;

namespace SS_Microservice.Services.Order.Infrastructure.Data.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("orderitems");

            builder
                .HasKey(x => x.OrderItemId);
            builder
                .Property(x => x.ProductId)
                .IsRequired();

            builder
                .Property(x => x.UnitPrice)
                .HasColumnType("DECIMAL")
                .IsRequired();
            builder
                .Property(x => x.TotalPrice)
                .HasColumnType("DECIMAL")
                .IsRequired();

            builder
                .Property(x => x.Quantity)
                .IsRequired();
            builder
                .Property(x => x.OrderId)
                .IsRequired();
            builder
                .Property(x => x.ProductId)
                .IsRequired();
            builder
                .HasOne(x => x.Order)
                .WithMany(x => x.OrderItems)
                .HasForeignKey(x => x.OrderId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}