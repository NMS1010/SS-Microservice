using AutoMapper;
using SS_Microservice.Services.Basket.Application.Dto;
using SS_Microservice.Services.Basket.Core.Entities;

namespace SS_Microservice.Services.Basket.Application.Common.AutoMapper
{
    public class BasketProfile : Profile
    {
        public BasketProfile()
        {
            CreateMap<Core.Entities.Basket, BasketDto>();
            CreateMap<BasketItem, BasketItemDto>();
        }
    }
}