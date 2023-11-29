using AutoMapper;
using SS_Microservice.Common.Exceptions;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Common.Repository;
using SS_Microservice.Services.Address.Application.Dto;
using SS_Microservice.Services.Address.Application.Features.Address.Commands;
using SS_Microservice.Services.Address.Application.Features.Address.Queries;
using SS_Microservice.Services.Address.Application.Features.District.Queries;
using SS_Microservice.Services.Address.Application.Features.Ward.Queries;
using SS_Microservice.Services.Address.Application.Interfaces;
using SS_Microservice.Services.Address.Application.Specifications;
using SS_Microservice.Services.Address.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace SS_Microservice.Services.Address.Infrastructure.Services
{
    public class AddressService : IAddressService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddressService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<long> CreateAddress(CreateAddressCommand command)
        {
            try
            {
                await _unitOfWork.CreateTransaction();

                var province = await _unitOfWork.Repository<Province>().GetById(command.ProvinceId)
                    ?? throw new NotFoundException("Cannot find province");

                var district = await _unitOfWork.Repository<District>().GetEntityWithSpec(new DistrictSpecification(command.DistrictId))
                    ?? throw new NotFoundException("Cannot find district");

                var ward = await _unitOfWork.Repository<Ward>().GetEntityWithSpec(new WardSpecification(command.WardId))
                    ?? throw new NotFoundException("Cannot find ward");

                if (ward.District.Id != district.Id || district.Province.Id != province.Id)
                    throw new ValidationException("Cannot identify this address");

                var address = _mapper.Map<Domain.Entities.Address>(command);
                address.Province = province;
                address.District = district;
                address.Ward = ward;
                address.IsDefault = true;

                var addresses = await _unitOfWork.Repository<Domain.Entities.Address>().ListAsync(new AddressSpecification(command.UserId, isDefault: true));

                foreach (var a in addresses)
                {
                    a.IsDefault = false;
                    _unitOfWork.Repository<Domain.Entities.Address>().Update(a);
                }

                await _unitOfWork.Repository<Domain.Entities.Address>().Insert(address);

                var isSuccess = await _unitOfWork.Save() > 0;
                await _unitOfWork.Commit();

                if (!isSuccess)
                {
                    throw new Exception("Cannot insert address for user");
                }

                return address.Id;
            }
            catch
            {
                await _unitOfWork.Rollback();
                throw;
            }
        }

        public async Task<bool> DeleteAddress(DeleteAddressCommand command)
        {
            var address = await _unitOfWork.Repository<Domain.Entities.Address>()
                .GetEntityWithSpec(new AddressSpecification(command.UserId, command.Id))
                ?? throw new NotFoundException("Cannot find this address");
            if (address.IsDefault)
                throw new ValidationException("Cannot delete default address, please set another address to default and try again");

            address.Status = false;

            _unitOfWork.Repository<Domain.Entities.Address>().Update(address);

            var isSuccess = await _unitOfWork.Save() > 0;

            if (!isSuccess)
            {
                throw new Exception("Cannot delete address");
            }

            return true;
        }

        public async Task<AddressDto> GetAddress(GetAddressQuery query)
        {
            var address = await _unitOfWork.Repository<Domain.Entities.Address>()
                .GetEntityWithSpec(new AddressSpecification(query.UserId, query.Id))
                ?? throw new NotFoundException("Cannot find this address");

            return _mapper.Map<AddressDto>(address);
        }

        public async Task<AddressDto> GetDefaultAddress(GetDefaultAddressQuery query)
        {
            var address = await _unitOfWork.Repository<Domain.Entities.Address>().GetEntityWithSpec(new AddressSpecification(query.UserId, true));
            if (address == null)
                return null;

            return _mapper.Map<AddressDto>(address);
        }

        public async Task<PaginatedResult<AddressDto>> GetListAddress(GetListAddressQuery query)
        {
            var spec = new AddressSpecification(query, isPaging: true);
            var countSpec = new AddressSpecification(query);

            var addresses = await _unitOfWork.Repository<Domain.Entities.Address>().ListAsync(spec);
            var count = await _unitOfWork.Repository<Domain.Entities.Address>().CountAsync(countSpec);

            var addressDtos = new List<AddressDto>();
            addresses.ForEach(x => addressDtos.Add(_mapper.Map<AddressDto>(x)));

            return new PaginatedResult<AddressDto>(addressDtos, query.PageIndex, count, query.PageSize);
        }

        public async Task<List<DistrictDto>> GetListDistrictByProvince(GetListDistrictByProvinceQuery query)
        {
            var province = await _unitOfWork.Repository<Province>().GetEntityWithSpec(new ProvinceSpecification(query.ProvinceId))
                ?? throw new NotFoundException("Cannot find province");

            var districtDtos = new List<DistrictDto>();

            province.Districts.ToList().ForEach(x => districtDtos.Add(_mapper.Map<DistrictDto>(x)));

            return districtDtos;
        }

        public async Task<List<ProvinceDto>> GetListProvince()
        {
            var provinces = (await _unitOfWork.Repository<Province>().GetAll()).ToList();

            var provinceDtos = new List<ProvinceDto>();

            provinces.ForEach(x => provinceDtos.Add(_mapper.Map<ProvinceDto>(x)));

            return provinceDtos;
        }

        public async Task<List<WardDto>> GetListWardByDistrict(GetListWardByDistrictQuery query)
        {
            var district = await _unitOfWork.Repository<District>()
                .GetEntityWithSpec(new DistrictSpecification(query.DistrictId))
                ?? throw new NotFoundException("Cannot find district");

            var wardDtos = new List<WardDto>();

            district.Wards.ToList().ForEach(x => wardDtos.Add(_mapper.Map<WardDto>(x)));

            return wardDtos;
        }

        public async Task<bool> SetAddressDefault(SetDefaultAddressCommand command)
        {
            try
            {
                await _unitOfWork.CreateTransaction();
                var address = await _unitOfWork.Repository<Domain.Entities.Address>()
                    .GetEntityWithSpec(new AddressSpecification(command.UserId, command.Id))
                    ?? throw new NotFoundException("Cannot find this address");

                address.IsDefault = true;

                var addresses = await _unitOfWork.Repository<Domain.Entities.Address>().ListAsync(new AddressSpecification(command.UserId, isDefault: true));
                foreach (var a in addresses)
                {
                    a.IsDefault = false;
                    _unitOfWork.Repository<Domain.Entities.Address>().Update(a);
                }

                _unitOfWork.Repository<Domain.Entities.Address>().Update(address);

                var isSuccess = await _unitOfWork.Save() > 0;

                await _unitOfWork.Commit();

                if (!isSuccess)
                {
                    throw new Exception("Cannot set default address");
                }

                return true;
            }
            catch
            {
                await _unitOfWork.Rollback();
                throw;
            }
        }

        public async Task<bool> UpdateAddress(UpdateAddressCommand command)
        {
            try
            {
                await _unitOfWork.CreateTransaction();

                var address = await _unitOfWork.Repository<Domain.Entities.Address>()
                    .GetEntityWithSpec(new AddressSpecification(command.UserId, command.Id))
                    ?? throw new NotFoundException("Cannot find address of this user");

                var province = await _unitOfWork.Repository<Province>().GetById(command.ProvinceId)
                    ?? throw new NotFoundException("Cannot find province");

                var district = await _unitOfWork.Repository<District>().GetEntityWithSpec(new DistrictSpecification(command.DistrictId))
                    ?? throw new NotFoundException("Cannot find district");

                var ward = await _unitOfWork.Repository<Ward>().GetEntityWithSpec(new WardSpecification(command.WardId))
                    ?? throw new NotFoundException("Cannot find ward");

                if (ward.District.Id != district.Id || district.Province.Id != province.Id)
                    throw new ValidationException("Cannot identify this address");

                _mapper.Map(command, address);

                address.Province = province;
                address.District = district;
                address.Ward = ward;
                if (!address.IsDefault)
                {
                    address.IsDefault = command.IsDefault;

                    if (command.IsDefault)
                    {
                        var addresses = await _unitOfWork.Repository<Domain.Entities.Address>()
                            .ListAsync(new AddressSpecification(address.UserId, isDefault: true));

                        foreach (var a in addresses)
                        {
                            a.IsDefault = false;
                            _unitOfWork.Repository<Domain.Entities.Address>().Update(a);
                        }
                    }
                }

                _unitOfWork.Repository<Domain.Entities.Address>().Update(address);

                var isSuccess = await _unitOfWork.Save() > 0;
                await _unitOfWork.Commit();

                if (!isSuccess)
                {
                    throw new Exception("Cannot update address for user");
                }

                return true;
            }
            catch
            {
                await _unitOfWork.Rollback();
                throw;
            }
        }
    }
}