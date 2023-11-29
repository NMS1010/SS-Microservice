using AutoMapper;
using SS_Microservice.Services.Products.Application.Dto;
using SS_Microservice.Services.Products.Application.Features.ProductImage.Commands;
using SS_Microservice.Services.Products.Application.Model.ProductImage;
using SS_Microservice.Services.Products.Domain.Entities;

namespace SS_Microservice.Services.Products.Application.Common.AutoMapper
{
    public class ProductImageMapperProfile : Profile
    {
        public ProductImageMapperProfile()
        {
            CreateMap<ProductImage, ProductImageDto>();
            CreateMap<ProductImageDto, ProductImage>();

            // mapping command/query - request

            CreateMap<CreateProductImageRequest, CreateProductImageCommand>();
            CreateMap<UpdateProductImageRequest, UpdateProductImageCommand>();
        }
    }
}