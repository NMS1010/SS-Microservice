using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SS_Microservice.Services.Order.Domain.Entities;

namespace SS_Microservice.Services.Order.Infrastructure.Data.Configurations
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("transaction");

            builder.HasKey(t => t.Id);

            builder.Property(x => x.PaymentMethodType)
                .IsRequired();

            builder.Property(x => x.OrderId)
                .IsRequired();

            builder.Property(x => x.PaidAt)
                .IsRequired(false);

            builder.Property(x => x.CompletedAt)
                .IsRequired(false);

            builder.Property(x => x.PaypalOrderId)
                .IsRequired(false);
            builder.Property(x => x.PaypalOrderStatus)
                .IsRequired(false);
            builder.Property(x => x.TotalPay)
                .HasColumnType("DECIMAL")
                .IsRequired();

            builder
                .HasOne(x => x.Order)
                .WithOne(x => x.Transaction)
                .HasForeignKey<Transaction>(x => x.OrderId);
        }
    }
}