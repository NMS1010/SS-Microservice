using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Services.Infrastructure.Application.Dto;
using SS_Microservice.Services.Infrastructure.Application.Features.Notification.Commands;
using SS_Microservice.Services.Infrastructure.Application.Features.Notification.Queries;

namespace SS_Microservice.Services.Infrastructure.Application.Interfaces
{
    public interface INotificationService
    {
        Task<bool> CreateNotification(CreateNotificationCommand command);

        Task<bool> UpdateNotificationStatus(long notificationId);

        Task<PaginatedResult<NotificationDto>> GetAllNotification(GetAllNotificationQuery query);
    }
}