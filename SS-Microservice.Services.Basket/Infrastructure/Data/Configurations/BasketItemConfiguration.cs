using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SS_Microservice.Services.Basket.Domain.Entities;

namespace SS_Microservice.Services.Basket.Infrastructure.Data.Configurations
{
    public class BasketItemConfiguration : IEntityTypeConfiguration<BasketItem>
    {
        public void Configure(EntityTypeBuilder<BasketItem> builder)
        {
            builder.Property(x => x.VariantId).IsRequired();
            builder.Property(x => x.Quantity).IsRequired();
        }
    }
}