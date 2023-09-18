using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SS_Microservice.Services.Order.Infrastructure.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Domain.Entities.Order>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Order> builder)
        {
            builder.ToTable("orders");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Email).IsRequired();
            builder.Property(x => x.Phone).IsRequired();
            builder.Property(x => x.Address).IsRequired();

            builder.Property(x => x.DateDone).IsRequired(false);
            builder.Property(x => x.DatePaid).IsRequired(false);
            builder
                .Property(x => x.TotalPrice)
                .HasColumnType("DECIMAL")
                .IsRequired();
            builder
                .Property(x => x.TotalItemPrice)
                .HasColumnType("DECIMAL")
                .IsRequired();
            builder
                .HasMany(x => x.OrderItems)
                .WithOne(x => x.Order)
                .HasForeignKey(x => x.OrderId);

            builder
                .HasOne(x => x.OrderState)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.OrderStateId);
        }
    }
}