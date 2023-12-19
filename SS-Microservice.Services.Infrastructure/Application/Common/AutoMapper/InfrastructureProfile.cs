using AutoMapper;
using SS_Microservice.Services.Infrastructure.Application.Dto;
using SS_Microservice.Services.Infrastructure.Application.Features.Notification.Commands;
using SS_Microservice.Services.Infrastructure.Application.Features.Notification.Queries;
using SS_Microservice.Services.Infrastructure.Application.Model.Notification;
using SS_Microservice.Services.Infrastructure.Domain.Entities;

namespace SS_Microservice.Services.Infrastructure.Application.Common.AutoMapper
{
    public class InfrastructureProfile : Profile
    {
        public InfrastructureProfile()
        {
            CreateMap<Notification, NotificationDto>();

            CreateMap<CreateOrderNotificationCommand, Notification>();

            // mapping request - command/query
            CreateMap<GetNotificationPagingRequest, GetListNotificationQuery>();
            CreateMap<UpdateNotificationRequest, UpdateNotificationCommand>();
            CreateMap<CreateNotificationRequest, CreateOrderNotificationCommand>();
        }
    }
}