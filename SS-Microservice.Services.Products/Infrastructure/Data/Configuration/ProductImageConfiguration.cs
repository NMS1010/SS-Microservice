using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SS_Microservice.Services.Products.Domain.Entities;

namespace SS_Microservice.Services.Products.Infrastructure.Data.Configuration
{
	public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
	{
		public void Configure(EntityTypeBuilder<ProductImage> builder)
		{
			builder.Property(x => x.Image).IsRequired();
			builder.Property(x => x.IsDefault).IsRequired();
		}
	}
}