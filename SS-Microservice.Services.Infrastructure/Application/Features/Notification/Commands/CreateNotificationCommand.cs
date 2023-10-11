using MediatR;

namespace SS_Microservice.Services.Infrastructure.Application.Features.Notification.Commands
{
    public class CreateNotificationCommand : IRequest<bool>
    {
        public string UserId { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public string Anchor { get; set; }
        public bool Status { get; set; }
    }

    public class CreateNotificationHandler : IRequestHandler<CreateNotificationCommand, bool>
    {
        public Task<bool> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}