using AutoMapper;
using SS_Microservice.Services.Products.Application.Dto;
using SS_Microservice.Services.Products.Application.Features.Variant.Commands;
using SS_Microservice.Services.Products.Application.Features.Variant.Queries;
using SS_Microservice.Services.Products.Application.Model.Variant;
using SS_Microservice.Services.Products.Domain.Entities;

namespace SS_Microservice.Services.Products.Application.Common.AutoMapper
{
    public class VariantMapperProfile : Profile
    {
        public VariantMapperProfile()
        {
            CreateMap<Variant, VariantDto>();
            CreateMap<CreateVariantRequest, Variant>();
            CreateMap<UpdateVariantRequest, Variant>();
            CreateMap<string, Variant>().ConstructUsing(str => new Variant { });

            // mapping command - request
            CreateMap<CreateVariantRequest, CreateVariantCommand>();
            CreateMap<UpdateVariantRequest, UpdateVariantCommand>();
            CreateMap<GetVariantPagingRequest, GetListVariantQuery>();
        }
    }
}