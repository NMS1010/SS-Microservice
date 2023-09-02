using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SS_Microservice.Services.Order.Core.Entities;

namespace SS_Microservice.Services.Order.Infrastructure.Data.Configurations
{
    public class OrderStateConfiguration : IEntityTypeConfiguration<OrderState>
    {
        public void Configure(EntityTypeBuilder<OrderState> builder)
        {
            builder.ToTable("orderstate");

            builder.HasKey(x => x.OrderStateId);

            builder
                .Property(x => x.OrderStateName)
                .HasMaxLength(100)
                .IsRequired();

            builder
                .HasMany(x => x.Orders)
                .WithOne(x => x.OrderState)
                .HasForeignKey(x => x.OrderStateId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}