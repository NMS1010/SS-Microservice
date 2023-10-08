using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Services.Address.Application.Dto;
using SS_Microservice.Services.Address.Application.Features.Address.Commands;
using SS_Microservice.Services.Address.Application.Features.Address.Queries;
using SS_Microservice.Services.Address.Application.Features.District.Queries;
using SS_Microservice.Services.Address.Application.Features.Province.Queries;
using SS_Microservice.Services.Address.Application.Features.Ward.Queries;

namespace SS_Microservice.Services.Address.Application.Interfaces
{
    public interface IAddressService
    {
        Task<PaginatedResult<AddressDto>> GetAddressList(GetAllAddressQuery query);

        Task<AddressDto> GetAddress(GetAddressByIdQuery query);

        Task<bool> CreateAddress(CreateAddressCommand command);

        Task<bool> UpdateAddress(UpdateAddressCommand command);

        Task<bool> DeleteAddress(DeleteAddressCommand command);

        Task<PaginatedResult<ProvinceDto>> GetProvinceList(GetAllProvinceQuery query);

        Task<PaginatedResult<DistrictDto>> GetDistrictListByProvince(GetDistrictByProvinceIdQuery query);

        Task<PaginatedResult<WardDto>> GetWardListByDistrict(GetWardByDistrictIdQuery query);
    }
}