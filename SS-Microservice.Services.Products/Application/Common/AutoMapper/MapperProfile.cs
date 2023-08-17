using AutoMapper;
using SS_Microservice.Services.Products.Application.Dto;
using SS_Microservice.Services.Products.Application.Model.Product;
using SS_Microservice.Services.Products.Application.Product.Commands;
using SS_Microservice.Services.Products.Application.Product.Queries;
using SS_Microservice.Services.Products.Core.Entities;

namespace SS_Microservice.Services.Products.Application.Common.AutoMapper
{
    public class MapperProfile : Profile
    {
        private const string USER_CONTENT_FOLDER = "user-content";

        public string GetFile(string filename, IHttpContextAccessor httpContextAccessor)
        {
            var req = httpContextAccessor.HttpContext.Request;
            var path = $"{req.Scheme}://{req.Host}/{USER_CONTENT_FOLDER}/{filename}";
            return path;
        }

        public MapperProfile(IHttpContextAccessor httpContextAccessor)
        {
            CreateMap<Core.Entities.Product, ProductDTO>()
                .ForMember(des => des.MainImage,
                act => act.MapFrom(src => GetFile(src.MainImage, httpContextAccessor)));
            CreateMap<ProductCreateRequest, ProductCreateCommand>();
            CreateMap<ProductUpdateRequest, ProductUpdateCommand>();
            CreateMap<ProductPagingRequest, ProductGetAllQuery>();
            CreateMap<ProductImageUpdateRequest, ProductImageUpdateCommand>();
            CreateMap<ProductImage, ProductImageDTO>()
                .ForMember(des => des.Path,
                act => act.MapFrom(src => GetFile(src.ImageName, httpContextAccessor)));
        }
    }
}