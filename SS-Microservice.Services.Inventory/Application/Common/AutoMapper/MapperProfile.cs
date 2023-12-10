using AutoMapper;
using SS_Microservice.Services.Inventory.Application.Dto;
using SS_Microservice.Services.Inventory.Application.Features.Docket.Commands;
using SS_Microservice.Services.Inventory.Application.Features.Product.Commands;
using SS_Microservice.Services.Inventory.Application.Models.Inventory;

namespace SS_Microservice.Services.Inventory.Application.Common.AutoMapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Domain.Entities.Docket, DocketDto>().ReverseMap();

            // mapping request - command
            CreateMap<ImportProductRequest, ImportProductCommand>();
            CreateMap<ImportProductCommand, UpdateProductQuantityCommand>();
        }
    }
}
