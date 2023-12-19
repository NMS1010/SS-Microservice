using AutoMapper;
using SS_Microservice.Contracts.Commands.Inventory;
using SS_Microservice.Contracts.Events.Order;
using SS_Microservice.Services.Inventory.Application.Dto;
using SS_Microservice.Services.Inventory.Application.Features.Docket.Commands;
using SS_Microservice.Services.Inventory.Application.Features.Product.Commands;
using SS_Microservice.Services.Inventory.Application.Models.Inventory;
using SS_Microservice.Services.Inventory.Infrastructure.Consumers.Commands.OrderingSaga;
using SS_Microservice.Services.Inventory.Infrastructure.Consumers.Events.Order;

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

            // mapping messaging
            CreateMap<IExportInventoryCommand, ExportInventoryCommand>();
            CreateMap<IRollBackInventoryCommand, RollBackInventoryCommand>();
            CreateMap<IOrderCancelledEvent, ImportInventoryCommand>();
        }
    }
}
