using MediatR;
using SS_Microservice.Services.Infrastructure.Application.Interfaces;
using SS_Microservice.Services.Infrastructure.Application.Model.Notification;

namespace SS_Microservice.Services.Infrastructure.Application.Features.Notification.Commands
{
    public class UpdateNotificationCommand : UpdateNotificationRequest, IRequest<bool>
    {
    }

    public class UpdateNotificationHandler : IRequestHandler<UpdateNotificationCommand, bool>
    {
        private readonly INotificationService _notificationService;

        public UpdateNotificationHandler(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public async Task<bool> Handle(UpdateNotificationCommand request, CancellationToken cancellationToken)
        {
            return await _notificationService.UpdateNotification(request);
        }
    }
}
