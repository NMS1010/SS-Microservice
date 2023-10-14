using AutoMapper;
using SS_Microservice.Services.Address.Application.Dto;
using SS_Microservice.Services.Address.Application.Features.Address.Commands;
using SS_Microservice.Services.Address.Application.Features.Address.Queries;
using SS_Microservice.Services.Address.Application.Models.Address;
using SS_Microservice.Services.Address.Domain.Entities;

namespace SS_Microservice.Services.Address.Application.Common.AutoMapper
{
    public class AddressProfile : Profile
    {
        public AddressProfile()
        {
            CreateMap<Domain.Entities.Address, AddressDto>();
            CreateMap<Ward, WardDto>();
            CreateMap<District, DistrictDto>();
            CreateMap<Province, ProvinceDto>();
            CreateMap<CreateAddressCommand, Domain.Entities.Address>();
            CreateMap<UpdateAddressCommand, Domain.Entities.Address>();

            CreateMap<CreateAddressRequest, CreateAddressCommand>();
            CreateMap<UpdateAddressRequest, UpdateAddressCommand>();
            CreateMap<GetAddressPagingRequest, GetListAddressQuery>();
        }
    }
}