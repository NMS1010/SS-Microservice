using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SS_Microservice.Services.Products.Domain.Entities;

namespace SS_Microservice.Services.Products.Infrastructure.Data.Configuration
{
	public class VariantConfiguration : IEntityTypeConfiguration<Variant>
	{
		public void Configure(EntityTypeBuilder<Variant> builder)
		{
			builder.HasIndex(u => u.Sku).IsUnique();
			builder.Property(x => x.ItemPrice).HasColumnType("DECIMAL").IsRequired();
			builder.Property(x => x.PromotionalItemPrice).HasColumnType("DECIMAL");
			builder.Property(x => x.Name).IsRequired();
			builder.Property(x => x.Quantity).IsRequired();
			builder.Property(x => x.TotalPrice).HasColumnType("DECIMAL").IsRequired();
			builder.Property(x => x.Status).IsRequired();
		}
	}
}