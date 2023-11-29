using AutoMapper;
using SS_Microservice.Services.Products.Application.Dto;
using SS_Microservice.Services.Products.Application.Features.Sale.Commands;
using SS_Microservice.Services.Products.Application.Features.Sale.Queries;
using SS_Microservice.Services.Products.Application.Model.Sale;
using SS_Microservice.Services.Products.Domain.Entities;

namespace SS_Microservice.Services.Products.Application.Common.AutoMapper
{
    public class SaleMapperProfile : Profile
    {
        public SaleMapperProfile()
        {
            CreateMap<Sale, SaleDto>();
            CreateMap<CreateSaleRequest, Sale>();
            CreateMap<UpdateSaleRequest, Sale>().ForMember(dest => dest.Image, act => act.Ignore());

            // mapping command/query - request
            CreateMap<CreateSaleRequest, CreateSaleCommand>();
            CreateMap<UpdateSaleRequest, UpdateSaleCommand>();
            CreateMap<GetSalePagingRequest, GetListSaleQuery>();
        }
    }
}