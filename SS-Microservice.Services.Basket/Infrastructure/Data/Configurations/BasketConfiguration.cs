using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SS_Microservice.Services.Basket.Core.Entities;

namespace SS_Microservice.Services.Basket.Infrastructure.Data.Configurations
{
    public class BasketConfiguration : IEntityTypeConfiguration<Core.Entities.Basket>
    {
        public void Configure(EntityTypeBuilder<Core.Entities.Basket> builder)
        {
            builder.ToTable("baskets");

            builder.HasKey(x => x.BasketId);

            builder.Property(x => x.UserId).IsRequired();

            builder
                .HasMany(x => x.BasketItems)
                .WithOne(x => x.Basket)
                .HasForeignKey(x => x.BasketId);
        }
    }
}