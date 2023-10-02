using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SS_Microservice.Services.Address.Domain.Entities;

namespace SS_Microservice.Services.Address.Infrastructure.Data.Configurations
{
    public class DistrictConfiguration : IEntityTypeConfiguration<District>
    {
        public void Configure(EntityTypeBuilder<District> builder)
        {
            builder.ToTable(nameof(District));

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name).IsRequired();
            builder.Property(p => p.Code).IsRequired();

            builder
                .HasMany(x => x.Addresses)
                .WithOne(x => x.District)
                .HasForeignKey(x => x.DistrictId);

            builder
                .HasMany(x => x.Wards)
                .WithOne(x => x.District)
                .HasForeignKey(x => x.DistrictId);
        }
    }
}