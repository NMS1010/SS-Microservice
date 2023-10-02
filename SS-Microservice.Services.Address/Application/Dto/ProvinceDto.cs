using SS_Microservice.Common.Entities;

namespace SS_Microservice.Services.Address.Application.Dto
{
    public class ProvinceDto : BaseAuditableEntity<long>
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }
}