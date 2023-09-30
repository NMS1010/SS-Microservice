using SS_Microservice.Services.Auth.Application.Dto;
using SS_Microservice.Services.Auth.Application.Features.User.Commands;
using SS_Microservice.Services.Auth.Application.Features.User.Queries;

namespace SS_Microservice.Services.Auth.Application.Interfaces
{
    public interface IUserService
    {
        public Task<UserDto> GetUser(GetUserQuery query);

        public Task<bool> UpdateUser(UserUpdateCommand command);
    }
}