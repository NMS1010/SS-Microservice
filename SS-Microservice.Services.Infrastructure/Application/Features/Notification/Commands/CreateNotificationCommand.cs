﻿using MediatR;
using SS_Microservice.Services.Infrastructure.Application.Interfaces;
using SS_Microservice.Services.Infrastructure.Application.Model.Notification;

namespace SS_Microservice.Services.Infrastructure.Application.Features.Notification.Commands
{
    public class CreateNotificationCommand : CreateNotificationRequest, IRequest
    {
    }

    public class CreateNotificationHandler : IRequestHandler<CreateNotificationCommand>
    {
        private readonly INotificationService _notificationService;

        public CreateNotificationHandler(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public async Task Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
        {
            await _notificationService.CreateOrderNotification(request);
        }
    }
}