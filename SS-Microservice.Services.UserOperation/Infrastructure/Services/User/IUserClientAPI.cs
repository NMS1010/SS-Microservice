using RestEase;
using SS_Microservice.Common.Types.Model.CustomResponse;
using SS_Microservice.Services.UserOperation.Infrastructure.Services.User.Model.Response;

namespace SS_Microservice.Services.UserOperation.Infrastructure.Services.User
{
    public interface IUserClientAPI
    {
        [Get("api/users/{id}")]
        Task<CustomAPIResponse<UserDto>> GetUser([Path] string id);
    }
}
