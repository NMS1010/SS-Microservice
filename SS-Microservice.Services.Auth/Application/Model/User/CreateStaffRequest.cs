namespace SS_Microservice.Services.Auth.Application.Model.User
{
    public class CreateStaffRequest
    {
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Dob { get; set; }
        public string Gender { get; set; }
        public string Type { get; set; }
        public int Status { get; set; }
        public CreateAddressRequest Address { get; set; }
    }

    public class CreateAddressRequest
    {
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