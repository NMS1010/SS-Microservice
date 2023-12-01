using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SS_Microservice.Services.Basket.Infrastructure.Data.Configurations
{
    public class BasketConfiguration : IEntityTypeConfiguration<Domain.Entities.Basket>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Basket> builder)
        {
            builder.Property(x => x.UserId).IsRequired();
        }
    }
}