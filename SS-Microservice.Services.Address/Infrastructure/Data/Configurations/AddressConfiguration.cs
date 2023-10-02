using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SS_Microservice.Services.Address.Domain.Entities;

namespace SS_Microservice.Services.Address.Infrastructure.Data.Configurations
{
    public class AddressConfiguration : IEntityTypeConfiguration<Domain.Entities.Address>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Address> builder)
        {
            builder.ToTable(nameof(Domain.Entities.Address));

            builder.HasKey(x => x.Id);
            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.Street).IsRequired();
            builder.Property(x => x.Receiver).IsRequired();
            builder.Property(x => x.Phone).IsRequired();
            builder.Property(x => x.Email).IsRequired();
            builder.Property(x => x.IsDefault).IsRequired();

            builder
                .HasOne(x => x.Province)
                .WithMany(x => x.Addresses)
                .HasForeignKey(x => x.ProvinceId);

            builder
                .HasOne(x => x.District)
                .WithMany(x => x.Addresses)
                .HasForeignKey(x => x.DistrictId);

            builder
                .HasOne(x => x.Ward)
                .WithMany(x => x.Addresses)
                .HasForeignKey(x => x.WardId);
        }
    }
}