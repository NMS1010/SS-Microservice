using AutoMapper;
using SS_Microservice.Common.Messages.Events.User;
using SS_Microservice.Services.Basket.Application.Basket.Commands;
using SS_Microservice.Services.Basket.Application.Basket.Queries;
using SS_Microservice.Services.Basket.Application.Dto;
using SS_Microservice.Services.Basket.Application.Model;
using SS_Microservice.Services.Basket.Core.Entities;

namespace SS_Microservice.Services.Basket.Application.Common.AutoMapper
{
    public class BasketProfile : Profile
    {
        public BasketProfile()
        {
            CreateMap<Core.Entities.Basket, BasketDto>();
            CreateMap<BasketItem, BasketItemDto>();
            CreateMap<BasketPagingRequest, BasketGetQuery>();

            CreateMap<UserRegisted, BasketCreateCommand>();
        }
    }
}