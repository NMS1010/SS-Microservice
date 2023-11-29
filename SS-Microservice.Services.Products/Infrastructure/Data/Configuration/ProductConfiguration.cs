using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SS_Microservice.Services.Products.Domain.Entities;

namespace SS_Microservice.Services.Products.Infrastructure.Data.Configuration
{
	public class ProductConfiguration : IEntityTypeConfiguration<Product>
	{
		public void Configure(EntityTypeBuilder<Product> builder)
		{
			builder.HasIndex(u => u.Name).IsUnique();
			builder.HasIndex(u => u.Slug).IsUnique();
			builder.HasIndex(u => u.Code).IsUnique();
			builder.Property(x => x.Cost).HasColumnType("DECIMAL").IsRequired();
			builder.Property(x => x.ShortDescription).IsRequired();
			builder.Property(x => x.Description).IsRequired();
			builder.Property(x => x.Quantity).IsRequired();
			builder.Property(x => x.Sold).IsRequired();
			builder.Property(x => x.Rating).IsRequired();
			builder.Property(x => x.Slug).IsRequired();
			builder.Property(x => x.Status).IsRequired();
		}
	}
}