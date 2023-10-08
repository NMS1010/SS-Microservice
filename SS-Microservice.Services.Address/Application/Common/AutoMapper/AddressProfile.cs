using AutoMapper;
using SS_Microservice.Services.Address.Application.Dto;
using SS_Microservice.Services.Address.Application.Features.Address.Commands;
using SS_Microservice.Services.Address.Application.Features.Address.Queries;
using SS_Microservice.Services.Address.Application.Features.District.Queries;
using SS_Microservice.Services.Address.Application.Features.Province.Queries;
using SS_Microservice.Services.Address.Application.Features.Ward.Queries;
using SS_Microservice.Services.Address.Application.Models.Address;
using SS_Microservice.Services.Address.Application.Models.District;
using SS_Microservice.Services.Address.Application.Models.Province;
using SS_Microservice.Services.Address.Application.Models.Ward;
using SS_Microservice.Services.Address.Domain.Entities;

namespace SS_Microservice.Services.Address.Application.Common.AutoMapper
{
    public class AddressProfile : Profile
    {
        protected AddressProfile()
        {
            CreateMap<Ward, WardDto>();
            CreateMap<District, DistrictDto>();
            CreateMap<Province, ProvinceDto>();
            CreateMap<Domain.Entities.Address, AddressDto>();

            CreateMap<CreateAddressRequest, CreateAddressCommand>();
            CreateMap<UpdateAddressRequest, UpdateAddressCommand>();
            CreateMap<GetAddressPagingRequest, GetAllAddressQuery>();

            CreateMap<CreateAddressCommand, Domain.Entities.Address>();

            CreateMap<GetProvincePagingRequest, GetAllProvinceQuery>();
            CreateMap<GetDistrictPagingRequest, GetDistrictByProvinceIdQuery>();
            CreateMap<GetWardPagingRequest, GetWardByDistrictIdQuery>();
        }
    }
}