using AutoMapper;
using SS_Microservice.Services.Products.Application.Dto;
using SS_Microservice.Services.Products.Application.Features.Category.Commands;
using SS_Microservice.Services.Products.Application.Features.Product.Commands;
using SS_Microservice.Services.Products.Application.Features.Category.Queries;
using SS_Microservice.Services.Products.Application.Features.Product.Queries;
using SS_Microservice.Services.Products.Application.Model.Category;
using SS_Microservice.Services.Products.Application.Model.Product;
using SS_Microservice.Services.Products.Domain.Entities;

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
            CreateMap<Product, ProductDTO>()
                .ForMember(des => des.MainImage,
                act => act.MapFrom(src => GetFile(src.MainImage, httpContextAccessor)));
            CreateMap<ProductCreateRequest, CreateProductCommand>();
            CreateMap<ProductUpdateRequest, UpdateProductCommand>();
            CreateMap<ProductPagingRequest, GetAllProductQuery>();
            CreateMap<ProductImageUpdateRequest, UpdateProductImageCommand>();
            CreateMap<ProductImage, ProductImageDTO>()
                .ForMember(des => des.Path,
                act => act.MapFrom(src => GetFile(src.ImageName, httpContextAccessor)));

            CreateMap<Category, CategoryDTO>()
                .ForMember(des => des.Image,
                act => act.MapFrom(src => GetFile(src.Image, httpContextAccessor)));
            CreateMap<CategoryCreateRequest, CreateCategoryCommand>();
            CreateMap<CategoryUpdateRequest, UpdateCategoryCommand>();
            CreateMap<CategoryPagingRequest, GetAllCategoryQuery>();
        }
    }
}