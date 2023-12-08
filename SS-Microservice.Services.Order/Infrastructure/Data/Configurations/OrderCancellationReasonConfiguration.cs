using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SS_Microservice.Services.Order.Domain.Entities;

namespace SS_Microservice.Services.Order.Infrastructure.Data.Configurations
{
    public class OrderCancellationReasonConfiguration : IEntityTypeConfiguration<OrderCancellationReason>
    {
        public void Configure(EntityTypeBuilder<OrderCancellationReason> builder)
        {
            builder.HasIndex(u => u.Name).IsUnique();
            builder.Property(x => x.Note).IsRequired();
            builder.Property(x => x.Status).IsRequired();
        }
    }
}