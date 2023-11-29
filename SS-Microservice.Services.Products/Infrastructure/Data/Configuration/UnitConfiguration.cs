using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SS_Microservice.Services.Products.Domain.Entities;

namespace SS_Microservice.Services.Products.Infrastructure.Data.Configuration
{
	public class UnitConfiguration : IEntityTypeConfiguration<Unit>
	{
		public void Configure(EntityTypeBuilder<Unit> builder)
		{
			builder.HasIndex(u => u.Name).IsUnique();
			builder.Property(x => x.Status).IsRequired();
		}
	}
}