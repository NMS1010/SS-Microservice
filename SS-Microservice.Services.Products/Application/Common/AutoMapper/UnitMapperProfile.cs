using AutoMapper;
using SS_Microservice.Services.Products.Application.Dto;
using SS_Microservice.Services.Products.Application.Features.Unit.Command;
using SS_Microservice.Services.Products.Application.Features.Unit.Query;
using SS_Microservice.Services.Products.Application.Model.Unit;
using SS_Microservice.Services.Products.Domain.Entities;

namespace SS_Microservice.Services.Products.Application.Common.AutoMapper
{
    public class UnitMapperProfile : Profile
    {
        public UnitMapperProfile()
        {
            CreateMap<Unit, UnitDto>();
            CreateMap<CreateUnitRequest, Unit>();
            CreateMap<UpdateUnitRequest, Unit>();

            // mapping command - request
            CreateMap<CreateUnitRequest, CreateUnitCommand>();
            CreateMap<UpdateUnitRequest, UpdateUnitCommand>();
            CreateMap<GetUnitPagingRequest, GetListUnitQuery>();
        }
    }
}