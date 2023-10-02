using SS_Microservice.Common.Entities;

namespace SS_Microservice.Services.Address.Domain.Entities
{
    public class Ward : BaseAuditableEntity<long>
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public long DistrictId { get; set; }
        public District District { get; set; }
        public ICollection<Address> Addresses { get; set; }
    }
}