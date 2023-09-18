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
            CreateMap<Domain.Entities.Basket, BasketDto>();
            CreateMap<BasketItem, BasketItemDto>();
            CreateMap<BasketPagingRequest, GetBasketQuery>();

            CreateMap<UserRegistedEvent, CreateBasketCommand>();
        }
    }
}