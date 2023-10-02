using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SS_Microservice.Services.Address.Domain.Entities;

namespace SS_Microservice.Services.Address.Infrastructure.Data.Configurations
{
    public class ProvinceConfiguration : IEntityTypeConfiguration<Province>
    {
        public void Configure(EntityTypeBuilder<Province> builder)
        {
            builder.ToTable(nameof(Province));

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name).IsRequired();
            builder.Property(p => p.Code).IsRequired();

            builder
                .HasMany(x => x.Addresses)
                .WithOne(x => x.Province)
                .HasForeignKey(x => x.ProvinceId);

            builder
                .HasMany(x => x.Districts)
                .WithOne(x => x.Province)
                .HasForeignKey(x => x.ProvinceId);
        }
    }
}