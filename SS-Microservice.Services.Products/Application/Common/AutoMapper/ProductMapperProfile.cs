using AutoMapper;
using SS_Microservice.Contracts.Commands.Product;
using SS_Microservice.Services.Products.Application.Dto;
using SS_Microservice.Services.Products.Application.Features.Product.Commands;
using SS_Microservice.Services.Products.Application.Features.Product.Queries;
using SS_Microservice.Services.Products.Application.Model.Product;
using SS_Microservice.Services.Products.Domain.Entities;
using SS_Microservice.Services.Products.Infrastructure.Consumers.Commands.Inventory;
using SS_Microservice.Services.Products.Infrastructure.Consumers.Commands.OrderingSaga;

namespace SS_Microservice.Services.Products.Application.Common.AutoMapper
{
    public class ProductMapperProfile : Profile
    {
        public ProductMapperProfile()
        {
            // Product
            CreateMap<Product, ProductDto>();
            CreateMap<CreateProductRequest, Product>().ForMember(dest => dest.Variants, act => act.Ignore());
            CreateMap<UpdateProductRequest, Product>();

            // mapping command/query - request
            CreateMap<CreateProductRequest, CreateProductCommand>();
            CreateMap<UpdateProductRequest, UpdateProductCommand>();
            CreateMap<GetProductPagingRequest, GetListProductQuery>();
            CreateMap<FilterProductPagingRequest, GetListFilteringProductQuery>();
            CreateMap<SearchProductPagingRequest, GetListSearchingProductQuery>();

            // mapping messaging
            CreateMap<IUpdateProductQuantityCommand, UpdateOneProductQuantityCommand>();
            CreateMap<IReserveStockCommand, ReserveStockCommand>();
            CreateMap<IRollBackStockCommand, RollBackStockCommand>();

        }
    }
}