using MediatR;
using SS_Microservice.Services.Auth.Application.Dto;

namespace SS_Microservice.Services.Auth.Application.User.Queries
{
    public class GetUserQuery : IRequest<UserDto>
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}