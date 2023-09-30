using MassTransit;
using MediatR;
using SS_Microservice.Common.Messages.Events.User;
using SS_Microservice.Services.Auth.Application.Interfaces;

namespace SS_Microservice.Services.Auth.Application.Features.Auth.Commands
{
    public class RegisterUserCommand : IRequest<bool>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; } = DateTime.Now;
        public int Status { get; set; } = 1;
    }

    public class RegisterHandler : IRequestHandler<RegisterUserCommand, bool>
    {
        private readonly IAuthService _authService;
        private readonly IBus _publisher;
        private readonly ILogger<RegisterHandler> _logger;

        public RegisterHandler(IAuthService authService, IBus publishEndPoint, ILogger<RegisterHandler> logger)
        {
            _authService = authService;
            _publisher = publishEndPoint;
            _logger = logger;
        }

        public async Task<bool> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var userId = await _authService.Register(request);
            if (!string.IsNullOrEmpty(userId))
            {
                _logger.LogInformation($"Start publishing message with userId = {userId}");
                await _publisher.Publish(new UserRegistedEvent()
                {
                    UserId = userId,
                });
                _logger.LogInformation($"Message is published");
            }
            return !string.IsNullOrEmpty(userId);
        }
    }
}