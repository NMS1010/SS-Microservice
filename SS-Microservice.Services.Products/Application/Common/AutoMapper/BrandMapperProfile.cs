using AutoMapper;
using SS_Microservice.Services.Products.Application.Dto;
using SS_Microservice.Services.Products.Application.Features.Brand.Commands;
using SS_Microservice.Services.Products.Application.Features.Brand.Queries;
using SS_Microservice.Services.Products.Application.Model.Brand;
using SS_Microservice.Services.Products.Domain.Entities;

namespace SS_Microservice.Services.Products.Application.Common.AutoMapper
{
    public class BrandMapperProfile : Profile
    {
        public BrandMapperProfile()
        {
            CreateMap<Brand, BrandDto>();
            CreateMap<CreateBrandCommand, Brand>();
            CreateMap<UpdateBrandCommand, Brand>().ForMember(dest => dest.Image, act => act.Ignore());

            // mapping command - request
            CreateMap<CreateBrandRequest, CreateBrandCommand>();
            CreateMap<UpdateBrandRequest, UpdateBrandCommand>();
            CreateMap<GetBrandPagingRequest, GetListBrandQuery>();
        }
    }
}