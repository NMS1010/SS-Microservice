using SS_Microservice.Contracts.Commands.Address;

namespace SS_Microservice.Services.Auth.Application.Features.Address.Commands
{
    public class UpdateAddressCommand : IUpdateAddressCommand
    {
        public string UserId { get; set; }
        public long Id { get; set; }
        public string Receiver { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Street { get; set; }
        public bool IsDefault { get; set; }
        public long ProvinceId { get; set; }
        public long DistrictId { get; set; }
        public long WardId { get; set; }
    }
}
