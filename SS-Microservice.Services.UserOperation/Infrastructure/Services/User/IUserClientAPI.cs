using RestEase;
using SS_Microservice.Services.UserOperation.Infrastructure.Services.User.Model.Response;

namespace SS_Microservice.Services.UserOperation.Infrastructure.Services.User
{
    public interface IUserClientAPI
    {
        [Get("api/users/{id}")]
        Task<UserDto> GetUser([Path] string id);
    }
}
