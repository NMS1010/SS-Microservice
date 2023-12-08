using MediatR;
using SS_Microservice.Services.Infrastructure.Application.Interfaces;

namespace SS_Microservice.Services.Infrastructure.Application.Features.Notification.Commands
{
    public class UpdateListNotificationCommand : IRequest<bool>
    {
        public string UserId { get; set; }
    }

    public class UpdateListNotificationHandler : IRequestHandler<UpdateListNotificationCommand, bool>
    {
        private readonly INotificationService _notificationService;

        public UpdateListNotificationHandler(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public async Task<bool> Handle(UpdateListNotificationCommand request, CancellationToken cancellationToken)
        {
            return await _notificationService.UpdateListNotification(request);
        }
    }
}
