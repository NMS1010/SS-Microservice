using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SS_Microservice.Services.Order.Domain.Entities;

namespace SS_Microservice.Services.Order.Infrastructure.Data.Configurations
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.HasIndex(u => u.PaypalOrderId).IsUnique();
            builder.Property(x => x.TotalPay).HasColumnType("DECIMAL").IsRequired();
            builder.Property(x => x.PaymentMethod).IsRequired();
        }
    }
}