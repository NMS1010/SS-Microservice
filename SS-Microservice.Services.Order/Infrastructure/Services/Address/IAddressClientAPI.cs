using RestEase;
using SS_Microservice.Common.Types.Model.CustomResponse;
using SS_Microservice.Services.Order.Infrastructure.Services.Address.Model.Response;

namespace SS_Microservice.Services.Order.Infrastructure.Services.Address
{
    public interface IAddressClientAPI
    {
        [Get("/api/addresses/internal/default/{userId}")]
        Task<CustomAPIResponse<AddressDto>> GetDefaultAddress([Path] string userId);


        [Get("/api/addresses/internal/{id}")]
        Task<CustomAPIResponse<AddressDto>> GetAddress([Path] long id);
    }
}
