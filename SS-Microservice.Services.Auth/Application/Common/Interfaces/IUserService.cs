using SS_Microservice.Services.Auth.Application.Dto;
using SS_Microservice.Services.Auth.Application.User.Commands;
using SS_Microservice.Services.Auth.Application.User.Queries;

namespace SS_Microservice.Services.Auth.Application.Common.Interfaces
{
    public interface IUserService
    {
        public Task<UserDto> GetUser(GetUserQuery query);

        public Task<bool> UpdateUser(UserUpdateCommand command);
    }
}