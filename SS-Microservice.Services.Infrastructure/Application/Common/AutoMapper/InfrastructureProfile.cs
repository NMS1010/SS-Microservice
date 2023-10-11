using AutoMapper;
using SS_Microservice.Services.Infrastructure.Application.Dto;
using SS_Microservice.Services.Infrastructure.Application.Features.Notification.Commands;
using SS_Microservice.Services.Infrastructure.Application.Features.Notification.Queries;
using SS_Microservice.Services.Infrastructure.Application.Model;
using SS_Microservice.Services.Infrastructure.Domain.Entities;

namespace SS_Microservice.Services.Infrastructure.Application.Common.AutoMapper
{
    public class InfrastructureProfile : Profile
    {
        protected InfrastructureProfile()
        {
            CreateMap<Notification, NotificationDto>();

            CreateMap<GetNotificationPagingRequest, GetAllNotificationQuery>();
            CreateMap<CreateNotificationCommand, Notification>();
        }
    }
}