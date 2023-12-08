using MediatR;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Services.Infrastructure.Application.Dto;
using SS_Microservice.Services.Infrastructure.Application.Interfaces;
using SS_Microservice.Services.Infrastructure.Application.Model.Notification;

namespace SS_Microservice.Services.Infrastructure.Application.Features.Notification.Queries
{
    public class GetListNotificationQuery : GetNotificationPagingRequest, IRequest<PaginatedResult<NotificationDto>>
    {
    }

    public class GetListNotificationHandler : IRequestHandler<GetListNotificationQuery, PaginatedResult<NotificationDto>>
    {
        private readonly INotificationService _notificationService;

        public GetListNotificationHandler(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public async Task<PaginatedResult<NotificationDto>> Handle(GetListNotificationQuery request, CancellationToken cancellationToken)
        {
            return await _notificationService.GetListNotification(request);
        }
    }
}