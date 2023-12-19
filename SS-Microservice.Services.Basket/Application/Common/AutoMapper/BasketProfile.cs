using AutoMapper;
using SS_Microservice.Common.Grpc.Product.Protos;
using SS_Microservice.Contracts.Commands.Basket;
using SS_Microservice.Contracts.Events.User;
using SS_Microservice.Services.Basket.Application.Dto;
using SS_Microservice.Services.Basket.Application.Features.Basket.Commands;
using SS_Microservice.Services.Basket.Application.Features.Basket.Queries;
using SS_Microservice.Services.Basket.Application.Model;
using SS_Microservice.Services.Basket.Domain.Entities;
using SS_Microservice.Services.Basket.Infrastructure.Consumers.Commands.OrderingSaga;
using SS_Microservice.Services.Basket.Infrastructure.Consumers.Events.User;

namespace SS_Microservice.Services.Basket.Application.Common.AutoMapper
{
    public class BasketProfile : Profile
    {
        public BasketProfile()
        {
            CreateMap<BasketItem, BasketItemDto>();
            CreateMap<ProductCustomGrpcResponse, BasketItemDto>();

            // mappping request - command
            CreateMap<CreateBasketItemRequest, CreateBasketItemCommand>();
            CreateMap<UpdateBasketItemRequest, UpdateBasketItemCommand>();
            CreateMap<GetBasketPagingRequest, GetListBasketByUserQuery>();

            // mapping messaging
            CreateMap<IUserRegistedEvent, CreateBasketCommand>();

            CreateMap<IClearBasketCommand, ClearBasketCommand>();
        }
    }
}