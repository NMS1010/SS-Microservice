using RestEase;
using SS_Microservice.Common.Types.Model.CustomResponse;
using SS_Microservice.Common.Types.Model.Paging;
using SS_Microservice.Services.Auth.Infrastructure.Services.Address.Model.Request;
using SS_Microservice.Services.Auth.Infrastructure.Services.Address.Model.Response;

namespace SS_Microservice.Services.Auth.Infrastructure.Services.Address
{
    public interface IAddressClientAPI
    {
        [Get("/api/addresses/internal/user/{userId}")]
        Task<CustomAPIResponse<PaginatedResult<AddressDto>>> GetListAddressByUser([Path] string userId, [Query] GetListAddressRequest request);
    }
}
