using AutoMapper;
using SS_Microservice.Services.Products.Application.Dto;
using SS_Microservice.Services.Products.Application.Features.Category.Commands;
using SS_Microservice.Services.Products.Application.Features.Category.Queries;
using SS_Microservice.Services.Products.Application.Model.Category;
using SS_Microservice.Services.Products.Domain.Entities;

namespace SS_Microservice.Services.Products.Application.Common.AutoMapper
{
    public class CategoryMapperProfile : Profile
    {
        public CategoryMapperProfile()
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<CreateCategoryCommand, Category>();
            CreateMap<UpdateCategoryCommand, Category>().ForMember(dest => dest.Image, act => act.Ignore());

            // mapping command - request
            CreateMap<CreateCategoryRequest, CreateCategoryCommand>();
            CreateMap<UpdateCategoryRequest, UpdateCategoryCommand>();
            CreateMap<GetCategoryPagingRequest, GetListCategoryQuery>();
        }
    }
}