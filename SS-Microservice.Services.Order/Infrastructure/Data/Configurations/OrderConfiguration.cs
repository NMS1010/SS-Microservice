using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SS_Microservice.Services.Order.Domain.Entities;

namespace SS_Microservice.Services.Order.Infrastructure.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Domain.Entities.Order>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Order> builder)
        {
            builder.ToTable("orders");

            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.UserId)
                .IsRequired();

            builder.Property(x => x.AddressId)
                .IsRequired();

            builder.Property(x => x.OtherCancelReason)
                .IsRequired(false);

            builder
                .Property(x => x.TotalAmount)
                .HasColumnType("DECIMAL")
                .IsRequired();

            builder
                .Property(x => x.Tax)
                .HasColumnType("DECIMAL")
                .IsRequired();

            builder
                .Property(x => x.ShippingCost)
                .HasColumnType("DECIMAL")
                .IsRequired();

            builder.Property(x => x.PaymentStatus)
                .IsRequired();

            builder.Property(x => x.Note)
                .IsRequired(false);

            builder.Property(x => x.Code)
                .IsRequired();

            builder.Property(x => x.DeliveryMethodType)
                .IsRequired();

            builder.Property(x => x.OrderStateId)
                .IsRequired();

            builder
                .HasMany(x => x.OrderItems)
                .WithOne(x => x.Order)
                .HasForeignKey(x => x.OrderId);

            builder
                .HasOne(x => x.OrderState)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.OrderStateId);
            builder
                .HasOne(x => x.OrderCancellationReason)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.OrderCancellationReasonId);
            builder
                .HasOne(x => x.Transaction)
                .WithOne(x => x.Order)
                .HasForeignKey<Transaction>(x => x.OrderId);
        }
    }
}