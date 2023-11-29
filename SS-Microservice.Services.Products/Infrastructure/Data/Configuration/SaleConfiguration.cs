using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SS_Microservice.Services.Products.Domain.Entities;

namespace SS_Microservice.Services.Products.Infrastructure.Data.Configuration
{
	public class SaleConfiguration : IEntityTypeConfiguration<Sale>
	{
		public void Configure(EntityTypeBuilder<Sale> builder)
		{
			builder.Property(x => x.Name).IsRequired();
			builder.Property(x => x.Image).IsRequired();
			builder.Property(x => x.StartDate).IsRequired();
			builder.Property(x => x.EndDate).IsRequired();
			builder.Property(x => x.PromotionalPercent).IsRequired();
			builder.Property(x => x.Slug).IsRequired();
			builder.Property(x => x.Status).IsRequired();
		}
	}
}