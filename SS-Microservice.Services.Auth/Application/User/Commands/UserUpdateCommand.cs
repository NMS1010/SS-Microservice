using MediatR;

namespace SS_Microservice.Services.Auth.Application.User.Commands
{
    public class UserUpdateCommand : IRequest<bool>
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public IFormFile Avatar { get; set; }
    }
}