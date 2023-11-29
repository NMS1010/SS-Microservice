using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SS_Microservice.Services.Products.Domain.Entities;

namespace SS_Microservice.Services.Products.Infrastructure.Data.Configuration
{
	public class BrandConfiguration : IEntityTypeConfiguration<Brand>
	{
		public void Configure(EntityTypeBuilder<Brand> builder)
		{
			builder.HasIndex(u => u.Name).IsUnique();
			builder.HasIndex(u => u.Code).IsUnique();
			builder.Property(x => x.Description).IsRequired();
			builder.Property(x => x.Image).IsRequired();
			builder.Property(x => x.Status).IsRequired();
		}
	}
}