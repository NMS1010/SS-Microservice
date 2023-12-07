using RestEase;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Services.Auth.Application.Model.CustomResponse;
using SS_Microservice.Services.Auth.Infrastructure.Services.Address.Model.Request;
using SS_Microservice.Services.Auth.Infrastructure.Services.Address.Model.Response;

namespace SS_Microservice.Services.Auth.Infrastructure.Services.Address
{
    public interface IAddressClientAPI
    {
        [Get("/api/addresses/internal")]
        Task<CustomAPIResponse<PaginatedResult<AddressDto>>> GetListAddressByUser([Body] GetListAddressRequest request);
    }
}
