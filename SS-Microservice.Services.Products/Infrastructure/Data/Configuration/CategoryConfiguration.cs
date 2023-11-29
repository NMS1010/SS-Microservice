using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SS_Microservice.Services.Products.Domain.Entities;

namespace SS_Microservice.Services.Products.Infrastructure.Data.Configuration
{
	public class CategoryConfiguration : IEntityTypeConfiguration<Category>
	{
		public void Configure(EntityTypeBuilder<Category> builder)
		{
			builder.HasIndex(u => u.Name).IsUnique();
			builder.HasIndex(u => u.Slug).IsUnique();

			builder.Property(x => x.ParentId).IsRequired(false);

			builder.Property(x => x.Image).IsRequired();

			builder.Property(x => x.Slug).IsRequired();

			builder.Property(x => x.Status).IsRequired();
		}
	}
}