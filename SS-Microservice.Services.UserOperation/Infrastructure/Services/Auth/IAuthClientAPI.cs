using RestEase;
using SS_Microservice.Common.Types.Model.CustomResponse;
using SS_Microservice.Services.UserOperation.Infrastructure.Services.Auth.Model.Response;

namespace SS_Microservice.Services.UserOperation.Infrastructure.Services.Auth
{
    public interface IAuthClientAPI
    {
        [Get("api/users/internal/{userId}")]
        Task<CustomAPIResponse<UserDto>> GetUser([Path] string userId);
    }
}
