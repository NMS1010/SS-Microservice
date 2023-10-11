using MediatR;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Services.Infrastructure.Application.Dto;
using SS_Microservice.Services.Infrastructure.Application.Model;

namespace SS_Microservice.Services.Infrastructure.Application.Features.Notification.Queries
{
    public class GetAllNotificationQuery : GetNotificationPagingRequest, IRequest<PaginatedResult<NotificationDto>>
    {
        public string UserId { get; set; }
    }

    public class GetAllNotificationHandler : IRequestHandler<GetAllNotificationQuery, PaginatedResult<NotificationDto>>
    {
        public Task<PaginatedResult<NotificationDto>> Handle(GetAllNotificationQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}