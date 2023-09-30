using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SS_Microservice.Services.Order.Domain.Entities;

namespace SS_Microservice.Services.Order.Infrastructure.Data.Configurations
{
    public class OrderCancellationReasonConfiguration : IEntityTypeConfiguration<OrderCancellationReason>
    {
        public void Configure(EntityTypeBuilder<OrderCancellationReason> builder)
        {
            builder.ToTable(nameof(OrderCancellationReason));

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Note).IsRequired(false);
            builder.Property(x => x.Status).IsRequired();

            builder.HasMany(x => x.Orders)
                .WithOne(x => x.OrderCancellationReason)
                .HasForeignKey(x => x.OrderCancellationReasonId);
        }
    }
}