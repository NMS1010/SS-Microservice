using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SS_Microservice.Services.Order.Domain.Entities;

namespace SS_Microservice.Services.Order.Infrastructure.Data.Configurations
{
    public class PaymentMethodConfiguration : IEntityTypeConfiguration<PaymentMethod>
    {
        public void Configure(EntityTypeBuilder<PaymentMethod> builder)
        {
            builder.HasIndex(u => u.Name).IsUnique();
            builder.HasIndex(u => u.Code).IsUnique();
            builder.Property(x => x.Image).IsRequired();
            builder.Property(x => x.Status).IsRequired();
        }
    }
}