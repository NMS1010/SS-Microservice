using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SS_Microservice.Services.Basket.Infrastructure.Data.Configurations
{
    public class BasketConfiguration : IEntityTypeConfiguration<Domain.Entities.Basket>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Basket> builder)
        {
            builder.ToTable("baskets");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.UserId).IsRequired();

            builder
                .HasMany(x => x.BasketItems)
                .WithOne(x => x.Basket)
                .HasForeignKey(x => x.BasketId);
        }
    }
}