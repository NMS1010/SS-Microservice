using MassTransit;
using MediatR;
using SS_Microservice.Common.Logging.Messaging;
using SS_Microservice.Common.RabbitMQ;
using SS_Microservice.Common.Types.Enums;
using SS_Microservice.Contracts.Commands.Mail;
using SS_Microservice.Contracts.Events.User;
using SS_Microservice.Services.Auth.Application.Common.Constants;
using SS_Microservice.Services.Auth.Application.Features.Auth.Events;
using SS_Microservice.Services.Auth.Application.Features.Mail.Command;
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
        private readonly IPublishEndpoint _publisher;
        private readonly ISendEndpointProvider _sendEndpoint;
        private readonly ILogger<RegisterHandler> _logger;
        private const string _handlerName = nameof(RegisterHandler);

        public RegisterHandler(IAuthService authService, IPublishEndpoint publishEndPoint,
            ILogger<RegisterHandler> logger, ISendEndpointProvider sendEndpoint)
        {
            _authService = authService;
            _publisher = publishEndPoint;
            _logger = logger;
            _sendEndpoint = sendEndpoint;
        }

        public async Task<string> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var userCreatedDto = await _authService.Register(request);
            if (userCreatedDto != null)
            {
                _logger.LogInformation(LoggerMessaging.StartPublishing(APPLICATION_SERVICE.AUTH_SERVICE, nameof(UserRegistedEvent), _handlerName));
                await _publisher.Publish<IUserRegistedEvent>(new UserRegistedEvent()
                {
                    Email = userCreatedDto.Email,
                    UserId = userCreatedDto.UserId,
                });
                _logger.LogInformation(LoggerMessaging.CompletePublishing(APPLICATION_SERVICE.AUTH_SERVICE, nameof(UserRegistedEvent), _handlerName));

                if (!string.IsNullOrEmpty(userCreatedDto.OTP))
                {
                    _logger.LogInformation(LoggerMessaging.StartPublishing(APPLICATION_SERVICE.AUTH_SERVICE, nameof(SendMailCommand), _handlerName));

                    await (await _sendEndpoint.GetSendEndpoint(new Uri($"queue:{EventBusConstant.SendMail}")))
                        .Send<ISendMailCommand>(new SendMailCommand()
                        {
                            To = userCreatedDto.Email,
                            Type = MAIL_TYPE.REGISTATION,
                            Payloads = new Dictionary<string, string>()
                            {
                                { "name", userCreatedDto.Name },
                                { "otp", userCreatedDto.OTP }
                            }
                        });

                    _logger.LogInformation(LoggerMessaging.CompletePublishing(APPLICATION_SERVICE.AUTH_SERVICE, nameof(SendMailCommand), _handlerName));
                }
            }
            return userCreatedDto.UserId;
        }
    }
}