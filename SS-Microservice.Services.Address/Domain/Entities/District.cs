using SS_Microservice.Common.Entities;

namespace SS_Microservice.Services.Address.Domain.Entities
{
    public class District : BaseAuditableEntity<long>
    {
        public string Name { get; set; }
        public string Code { get; set; }

        public long ProvinceId { get; set; }
        public Province Province { get; set; }
        public ICollection<Ward> Wards { get; set; }

        public ICollection<Address> Addresses { get; set; }
    }
}