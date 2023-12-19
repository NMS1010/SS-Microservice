using SS_Microservice.Common.Types.Model.Paging;
using SS_Microservice.Services.Infrastructure.Application.Dto;
using SS_Microservice.Services.Infrastructure.Application.Features.Notification.Commands;
using SS_Microservice.Services.Infrastructure.Application.Features.Notification.Queries;

namespace SS_Microservice.Services.Infrastructure.Application.Interfaces
{
    public interface INotificationService
    {
        Task CreateOrderNotification(CreateOrderNotificationCommand command);

        Task CreateSaleNotification(CreateOrderNotificationCommand command);

        Task<bool> UpdateNotification(UpdateNotificationCommand command);

        Task<bool> UpdateListNotification(UpdateListNotificationCommand command);

        Task<PaginatedResult<NotificationDto>> GetListNotification(GetListNotificationQuery query);
    }
}