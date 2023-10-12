using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Services.Address.Application.Dto;
using SS_Microservice.Services.Address.Application.Features.Address.Commands;
using SS_Microservice.Services.Address.Application.Features.Address.Queries;
using SS_Microservice.Services.Address.Application.Features.District.Queries;
using SS_Microservice.Services.Address.Application.Features.Province.Queries;
using SS_Microservice.Services.Address.Application.Features.Ward.Queries;
using SS_Microservice.Services.Address.Application.Models.Address;

namespace SS_Microservice.Services.Address.Application.Interfaces
{
    public interface IAddressService
    {
        public Task<long> CreateAddress(CreateAddressCommand command);

        public Task<bool> UpdateAddress(UpdateAddressCommand command);

        public Task<bool> DeleteAddress(DeleteAddressCommand command);

        public Task<AddressDto> GetAddress(GetAddressByIdQuery query);

        public Task<PaginatedResult<AddressDto>> GetListAddress(GetListAddressQuery query);

        public Task<bool> SetAddressDefault(SetDefaultAddressCommand command);

        Task<List<ProvinceDto>> GetListProvince();

        Task<List<DistrictDto>> GetListDistrictByProvince(GetListDistrictByProvinceIdQuery query);

        Task<List<WardDto>> GetListWardByDistrict(GetListWardByDistrictIdQuery query);
    }
}