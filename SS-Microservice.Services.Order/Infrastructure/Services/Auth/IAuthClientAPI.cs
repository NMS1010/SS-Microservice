using RestEase;
using SS_Microservice.Common.Types.Model.CustomResponse;
using SS_Microservice.Services.Order.Infrastructure.Services.Auth.Model.Response;

namespace SS_Microservice.Services.Order.Infrastructure.Services.Auth
{
    public interface IAuthClientAPI
    {
        [Get("/api/users/internal/{id}")]
        Task<CustomAPIResponse<UserDto>> GetUser([Path] string id);

        [Get("/api/users/internal/count/{role}")]
        Task<CustomAPIResponse<int>> GetTotalUserByRole([Path] string role);

    }
}
