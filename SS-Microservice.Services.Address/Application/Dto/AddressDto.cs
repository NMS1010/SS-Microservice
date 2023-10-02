using SS_Microservice.Common.Entities;
using SS_Microservice.Services.Address.Domain.Entities;

namespace SS_Microservice.Services.Address.Application.Dto
{
    public class AddressDto : BaseAuditableEntity<long>
    {
        public string UserId { get; set; }
        public string Receiver { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Street { get; set; }
        public bool IsDefault { get; set; }
        public int Status { get; set; }
        public ProvinceDto Province { get; set; }
        public DistrictDto District { get; set; }
        public WardDto Ward { get; set; }
    }
}