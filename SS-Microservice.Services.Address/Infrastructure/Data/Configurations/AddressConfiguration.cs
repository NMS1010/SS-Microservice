using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SS_Microservice.Services.Address.Infrastructure.Data.Configurations
{
    public class AddressConfiguration : IEntityTypeConfiguration<Domain.Entities.Address>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Address> builder)
        {
            builder.Property(x => x.Street).IsRequired();
            builder.Property(x => x.Receiver).IsRequired();
            builder.Property(x => x.Phone).IsRequired();
        }
    }
}