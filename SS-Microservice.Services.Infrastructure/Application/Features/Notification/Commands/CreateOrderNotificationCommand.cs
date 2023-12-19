using MediatR;
using SS_Microservice.Services.Infrastructure.Application.Interfaces;
using SS_Microservice.Services.Infrastructure.Application.Model.Notification;

namespace SS_Microservice.Services.Infrastructure.Application.Features.Notification.Commands
{
    public class CreateOrderNotificationCommand : CreateNotificationRequest, IRequest
    {
    }

    public class CreateNotificationHandler : IRequestHandler<CreateOrderNotificationCommand>
    {
        private readonly INotificationService _notificationService;

        public CreateNotificationHandler(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public async Task Handle(CreateOrderNotificationCommand request, CancellationToken cancellationToken)
        {
            await _notificationService.CreateOrderNotification(request);
        }
    }
}