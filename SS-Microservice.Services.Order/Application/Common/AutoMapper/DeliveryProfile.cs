using AutoMapper;
using SS_Microservice.Services.Order.Application.Dtos;
using SS_Microservice.Services.Order.Application.Features.Delivery.Commands;
using SS_Microservice.Services.Order.Application.Features.Delivery.Queries;
using SS_Microservice.Services.Order.Application.Models.Delivery;
using SS_Microservice.Services.Order.Domain.Entities;

namespace SS_Microservice.Services.Order.Application.Common.AutoMapper
{
    public class DeliveryProfile : Profile
    {
        public DeliveryProfile()
        {
            CreateMap<Delivery, DeliveryDto>();

            CreateMap<CreateDeliveryCommand, Delivery>();
            CreateMap<UpdateDeliveryCommand, Delivery>();

            // mapping request - command
            CreateMap<CreateDeliveryRequest, CreateDeliveryCommand>();
            CreateMap<UpdateDeliveryRequest, UpdateDeliveryCommand>();
            CreateMap<GetDeliveryPagingRequest, GetListDeliveryQuery>();
        }
    }
}
