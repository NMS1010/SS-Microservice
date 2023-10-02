using SS_Microservice.Common.Entities;

namespace SS_Microservice.Services.Address.Domain.Entities
{
    public class Province : BaseAuditableEntity<long>
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public ICollection<District> Districts { get; set; }
        public ICollection<Address> Addresses { get; set; }
    }
}