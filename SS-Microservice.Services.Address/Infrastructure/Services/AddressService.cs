using AutoMapper;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Common.Repository;
using SS_Microservice.Services.Address.Application.Dto;
using SS_Microservice.Services.Address.Application.Features.Address.Commands;
using SS_Microservice.Services.Address.Application.Features.Address.Queries;
using SS_Microservice.Services.Address.Application.Features.District.Queries;
using SS_Microservice.Services.Address.Application.Features.Province.Queries;
using SS_Microservice.Services.Address.Application.Features.Ward.Queries;
using SS_Microservice.Services.Address.Application.Interfaces;
using SS_Microservice.Services.Address.Application.Specifications;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

        public async Task<bool> CreateAddress(CreateAddressCommand command)
        {
            try
            {
                await _unitOfWork.CreateTransaction();
                var address = _mapper.Map<Domain.Entities.Address>(command);
                await _unitOfWork.Repository<Domain.Entities.Address>().Insert(address);

                var isSuccess = await _unitOfWork.Save() > 0;
                if (!isSuccess)
                {
                    throw new Exception("Cannot create address");
                }
                await _unitOfWork.Commit();

                return isSuccess;
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                throw ex;
            }
        }

        public async Task<bool> DeleteAddress(DeleteAddressCommand command)
        {
            try
            {
                await _unitOfWork.CreateTransaction();

                var repo = _unitOfWork.Repository<Domain.Entities.Address>();
                var address = await repo
                    .GetEntityWithSpec(new AddressSpecification(command.UserId, command.Id));
                address.Status = 0;

                repo.Update(address);

                var isSuccess = await _unitOfWork.Save() > 0;
                if (!isSuccess)
                {
                    throw new Exception("Cannot delete address");
                }
                await _unitOfWork.Commit();

                return isSuccess;
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                throw ex;
            }
        }

        public async Task<AddressDto> GetAddress(GetAddressByIdQuery query)
        {
            var address = await _unitOfWork.Repository<Domain.Entities.Address>()
                .GetEntityWithSpec(new AddressSpecification(query.UserId, query.Id));

            return _mapper.Map<AddressDto>(address);
        }

        public async Task<PaginatedResult<AddressDto>> GetAddressList(GetAllAddressQuery query)
        {
            var repo = _unitOfWork.Repository<Domain.Entities.Address>();
            var addressSpec = new AddressSpecification(query, isPaging: true);
            var addressCountSpec = new AddressSpecification(query);

            var addressList = await repo.ListAsync(addressSpec);
            var totalCount = await repo.CountAsync(addressCountSpec);

            var addressDtos = new List<AddressDto>();
            addressList.ForEach(x => addressDtos.Add(_mapper.Map<AddressDto>(x)));

            return new PaginatedResult<AddressDto>(addressDtos, (int)query.PageIndex, totalCount, (int)query.PageSize);
        }

        public async Task<PaginatedResult<DistrictDto>> GetDistrictListByProvince(GetDistrictByProvinceIdQuery query)
        {
            var repo = _unitOfWork.Repository<Domain.Entities.District>();
            var districtSpec = new DistrictSpecification(query, isPaging: true);
            var districtCountSpec = new DistrictSpecification(query);

            var districtList = await repo.ListAsync(districtSpec);
            var totalCount = await repo.CountAsync(districtCountSpec);

            var districtDtos = new List<DistrictDto>();
            districtList.ForEach(x => districtDtos.Add(_mapper.Map<DistrictDto>(x)));

            return new PaginatedResult<DistrictDto>(districtDtos, (int)query.PageIndex, totalCount, (int)query.PageSize);
        }

        public async Task<PaginatedResult<ProvinceDto>> GetProvinceList(GetAllProvinceQuery query)
        {
            var repo = _unitOfWork.Repository<Domain.Entities.Province>();
            var provinceSpec = new ProvinceSpecification(query, isPaging: true);
            var provinceCountSpec = new ProvinceSpecification(query);

            var provinceList = await repo.ListAsync(provinceSpec);
            var totalCount = await repo.CountAsync(provinceCountSpec);

            var provinceDtos = new List<ProvinceDto>();
            provinceList.ForEach(x => provinceDtos.Add(_mapper.Map<ProvinceDto>(x)));

            return new PaginatedResult<ProvinceDto>(provinceDtos, (int)query.PageIndex, totalCount, (int)query.PageSize);
        }

        public async Task<PaginatedResult<WardDto>> GetWardListByDistrict(GetWardByDistrictIdQuery query)
        {
            var repo = _unitOfWork.Repository<Domain.Entities.Ward>();
            var wardSpec = new WardSpecification(query, isPaging: true);
            var wardCountSpec = new WardSpecification(query);

            var wardList = await repo.ListAsync(wardSpec);
            var totalCount = await repo.CountAsync(wardCountSpec);

            var wardDtos = new List<WardDto>();
            wardList.ForEach(x => wardDtos.Add(_mapper.Map<WardDto>(x)));

            return new PaginatedResult<WardDto>(wardDtos, (int)query.PageIndex, totalCount, (int)query.PageSize);
        }

        public async Task<bool> UpdateAddress(UpdateAddressCommand command)
        {
            try
            {
                await _unitOfWork.CreateTransaction();

                var repo = _unitOfWork.Repository<Domain.Entities.Address>();
                var address = await repo.GetEntityWithSpec(new AddressSpecification(command.UserId, command.Id));

                address.Street = command.Street;
                address.ProvinceId = command.ProvinceId;
                address.DistrictId = command.DistrictId;
                address.WardId = command.WardId;
                address.Receiver = command.Receiver;
                address.Email = command.Email;
                address.Phone = command.Phone;
                address.IsDefault = command.IsDefault;

                repo.Update(address);

                var isSuccess = await _unitOfWork.Save() > 0;
                if (!isSuccess)
                {
                    throw new Exception("Cannot update address");
                }
                await _unitOfWork.Commit();

                return isSuccess;
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                throw ex;
            }
        }
    }
}