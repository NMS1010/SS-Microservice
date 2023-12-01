using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SS_Microservice.Services.Inventory.Domain.Entities;

namespace SS_Microservice.Services.Inventory.Infrastructure.Data.Configurations
{
    public class DocketConfiguration : IEntityTypeConfiguration<Docket>
    {
        public void Configure(EntityTypeBuilder<Docket> builder)
        {
            builder.Property(u => u.Type).IsRequired();
            builder.Property(u => u.Code).IsRequired();
            builder.HasIndex(u => u.Code).IsUnique();
            builder.Property(u => u.Quantity).IsRequired();
        }
    }
}
