using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SS_Microservice.Services.Address.Domain.Entities;

namespace SS_Microservice.Services.Address.Infrastructure.Data.Configurations
{
    public class ProvinceConfiguration : IEntityTypeConfiguration<Province>
    {
        public void Configure(EntityTypeBuilder<Province> builder)
        {
            builder.Property(x => x.Name).IsRequired();

            builder.Property(x => x.Code).IsRequired();
        }
    }
}