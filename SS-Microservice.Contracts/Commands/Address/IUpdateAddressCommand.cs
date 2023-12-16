using SS_Microservice.Common.Types.Messages;

namespace SS_Microservice.Contracts.Commands.Address
{
    public interface IUpdateAddressCommand : ICommand
    {
        string UserId { get; set; }
        long Id { get; set; }
        string Receiver { get; set; }
        string Phone { get; set; }
        string Email { get; set; }
        string Street { get; set; }
        bool IsDefault { get; set; }
        long ProvinceId { get; set; }
        long DistrictId { get; set; }
        long WardId { get; set; }
    }
}
