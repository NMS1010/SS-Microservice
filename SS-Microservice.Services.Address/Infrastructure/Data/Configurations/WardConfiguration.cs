using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SS_Microservice.Services.Address.Domain.Entities;

namespace SS_Microservice.Services.Address.Infrastructure.Data.Configurations
{
    public class WardConfiguration : IEntityTypeConfiguration<Ward>
    {
        public void Configure(EntityTypeBuilder<Ward> builder)
        {
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Code).IsRequired();
        }
    }
}