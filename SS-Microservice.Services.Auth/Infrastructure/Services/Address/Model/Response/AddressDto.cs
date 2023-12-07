using SS_Microservice.Common.Entities;

namespace SS_Microservice.Services.Auth.Infrastructure.Services.Address.Model.Response
{
    public class AddressDto : BaseAuditableEntity<long>
    {
        public string Receiver { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Street { get; set; }
        public bool IsDefault { get; set; }
        public bool Status { get; set; }
        public ProvinceDto Province { get; set; }
        public DistrictDto District { get; set; }
        public WardDto Ward { get; set; }
    }
    public class ProvinceDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }
    public class DistrictDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public ProvinceDto Province { get; set; }
    }
    public class WardDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public DistrictDto District { get; set; }
    }
}
