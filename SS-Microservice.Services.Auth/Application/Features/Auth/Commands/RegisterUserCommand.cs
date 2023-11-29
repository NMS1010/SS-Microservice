using MassTransit;
using MediatR;
using SS_Microservice.Common.Messages.Events.User;
using SS_Microservice.Services.Auth.Application.Interfaces;
using SS_Microservice.Services.Auth.Application.Model.Auth;

namespace SS_Microservice.Services.Auth.Application.Features.Auth.Commands
{
    public class RegisterUserCommand : RegisterRequest, IRequest<string>
    {
    }

    public class RegisterHandler : IRequestHandler<RegisterUserCommand, string>
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

        public async Task<string> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var userId = await _authService.Register(request);
            if (!string.IsNullOrEmpty(userId))
            {
                _logger.LogInformation($"[Auth Service] Start publishing UserRegisted event with userId = {userId}");
                await _publisher.Publish(new UserRegistedEvent()
                {
                    Email = request.Email,
                    UserId = userId,
                });
                _logger.LogInformation($"[Auth Service] UserRegistedEvent is published");
            }
            return userId;
        }
    }
}