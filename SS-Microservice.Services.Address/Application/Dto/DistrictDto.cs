using SS_Microservice.Common.Types.Entities;

namespace SS_Microservice.Services.Address.Application.Dto
{
    public class DistrictDto : BaseAuditableEntity<long>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public ProvinceDto Province { get; set; }
    }
}