using AutoMapper;
using SS_Microservice.Common.Messages.Events.User;
using SS_Microservice.Services.Basket.Application.Dto;
using SS_Microservice.Services.Basket.Application.Features.Basket.Commands;
using SS_Microservice.Services.Basket.Application.Features.Basket.Queries;
using SS_Microservice.Services.Basket.Application.Model;
using SS_Microservice.Services.Basket.Domain.Entities;

namespace SS_Microservice.Services.Basket.Application.Common.AutoMapper
{
    public class BasketProfile : Profile
    {
        public BasketProfile()
        {
            CreateMap<BasketItem, BasketItemDto>();

            // mappping request - command
            CreateMap<CreateBasketItemRequest, CreateBasketItemCommand>();
            CreateMap<UpdateBasketItemRequest, UpdateBasketItemCommand>();
            CreateMap<GetBasketPagingRequest, GetListBasketByUserQuery>();

            // mapping event - command
            CreateMap<UserRegistedEvent, CreateBasketCommand>();
        }
    }
}