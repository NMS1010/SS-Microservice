using SS_Microservice.Common.Types.Model.Paging;
using SS_Microservice.Services.Address.Application.Dto;
using SS_Microservice.Services.Address.Application.Features.Address.Commands;
using SS_Microservice.Services.Address.Application.Features.Address.Queries;
using SS_Microservice.Services.Address.Application.Features.District.Queries;
using SS_Microservice.Services.Address.Application.Features.Ward.Queries;

namespace SS_Microservice.Services.Address.Application.Interfaces
{
    public interface IAddressService
    {
        Task<long> CreateAddress(CreateAddressCommand command);

        Task<bool> UpdateAddress(UpdateAddressCommand command);

        Task<bool> DeleteAddress(DeleteAddressCommand command);

        Task<AddressDto> GetAddress(GetAddressQuery query);

        Task<AddressDto> GetDefaultAddress(GetDefaultAddressQuery query);

        Task<PaginatedResult<AddressDto>> GetListAddress(GetListAddressQuery query);

        Task<bool> SetAddressDefault(SetDefaultAddressCommand command);

        Task<List<ProvinceDto>> GetListProvince();

        Task<List<DistrictDto>> GetListDistrictByProvince(GetListDistrictByProvinceQuery query);

        Task<List<WardDto>> GetListWardByDistrict(GetListWardByDistrictQuery query);
    }
}