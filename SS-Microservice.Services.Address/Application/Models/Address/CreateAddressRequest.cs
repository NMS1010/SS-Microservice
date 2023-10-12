using System.Text.Json.Serialization;

namespace SS_Microservice.Services.Address.Application.Models.Address
{
    public class CreateAddressRequest
    {
        [JsonIgnore]
        public string UserId { get; set; }

        public string Receiver { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Street { get; set; }
        public long ProvinceId { get; set; }
        public long DistrictId { get; set; }
        public long WardId { get; set; }
    }
}