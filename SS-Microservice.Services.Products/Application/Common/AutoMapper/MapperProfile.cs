using AutoMapper;
using SS_Microservice.Services.Products.Application.Dto;
using SS_Microservice.Services.Products.Application.Features.Category.Commands;
using SS_Microservice.Services.Products.Application.Features.Product.Commands;
using SS_Microservice.Services.Products.Application.Features.Category.Queries;
using SS_Microservice.Services.Products.Application.Features.Product.Queries;
using SS_Microservice.Services.Products.Application.Model.Category;
using SS_Microservice.Services.Products.Application.Model.Product;
using SS_Microservice.Services.Products.Domain.Entities;
using SS_Microservice.Services.Products.Application.Features.Brand.Commands;
using SS_Microservice.Services.Products.Application.Model.Brand;
using SS_Microservice.Services.Products.Application.Features.Brand.Queries;
using SS_Microservice.Services.Products.Application.Features.Variant.Commands;
using SS_Microservice.Services.Products.Application.Model.Variant;
using SS_Microservice.Services.Products.Application.Features.Variant.Queries;

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
            CreateMap<Product, ProductDto>()
                .ForMember(des => des.PromotionalPrice,
                    act => act.MapFrom(src => src.Variants.Count > 0 ? src.Variants.Min(x => x.PromotionalItemPrice) : 0))
                .ForMember(des => des.Price,
                    act => act.MapFrom(src => src.Variants.Count > 0 ? src.Variants.Min(x => x.ItemPrice) : 0));

            CreateMap<Brand, BrandDto>();
            CreateMap<Variant, VariantDto>();
            CreateMap<ProductImage, ProductImageDto>()
                .ForMember(des => des.Image,
                act => act.MapFrom(src => GetFile(src.Image, httpContextAccessor)));

            CreateMap<Category, CategoryDto>()
                .ForMember(des => des.Image,
                act => act.MapFrom(src => GetFile(src.Image, httpContextAccessor)));

            CreateMap<CreateProductRequest, CreateProductCommand>();
            CreateMap<UpdateProductRequest, UpdateProductCommand>();
            CreateMap<GetProductPagingRequest, GetAllProductQuery>();
            CreateMap<CreateProductImageRequest, CreateProductImageCommand>();
            CreateMap<UpdateProductImageRequest, UpdateProductImageCommand>();

            CreateMap<CreateVariantRequest, CreateVariantCommand>();
            CreateMap<UpdateVariantRequest, UpdateVariantCommand>();
            CreateMap<GetVariantPagingRequest, GetAllVariantQuery>();

            CreateMap<CreateCategoryRequest, CreateCategoryCommand>();
            CreateMap<UpdateCategoryRequest, UpdateCategoryCommand>();
            CreateMap<GetCategoryPagingRequest, GetAllCategoryQuery>();

            CreateMap<CreateBrandRequest, CreateBrandCommand>();
            CreateMap<UpdateBrandRequest, UpdateBrandCommand>();
            CreateMap<GetBrandPagingRequest, GetAllBrandQuery>();
        }
    }
}