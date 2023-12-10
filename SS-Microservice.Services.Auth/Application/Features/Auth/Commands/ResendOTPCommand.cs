using MassTransit;
using MediatR;
using SS_Microservice.Common.Logging.Messaging;
using SS_Microservice.Common.Types.Enums;
using SS_Microservice.Contracts.Commands.Mail;
using SS_Microservice.Services.Auth.Application.Common.Constants;
using SS_Microservice.Services.Auth.Application.Features.Mail.Command;
using SS_Microservice.Services.Auth.Application.Interfaces;
using SS_Microservice.Services.Auth.Application.Model.Auth;

namespace SS_Microservice.Services.Auth.Application.Features.Auth.Commands
{
    public class ResendOTPCommand : ResendOTPRequest, IRequest<bool>
    {
    }

    public class ResendOTPHandler : IRequestHandler<ResendOTPCommand, bool>
    {
        private readonly IAuthService _authService;
        private readonly IPublishEndpoint _publisher;
        private readonly ILogger<ResendOTPHandler> _logger;
        private const string _handlerName = nameof(ResendOTPHandler);

        public ResendOTPHandler(IAuthService authService, IPublishEndpoint publisher, ILogger<ResendOTPHandler> logger)
        {
            _authService = authService;
            _publisher = publisher;
            _logger = logger;
        }

        public async Task<bool> Handle(ResendOTPCommand request, CancellationToken cancellationToken)
        {
            var res = await _authService.ResendOTP(request);
            if (res != null && !string.IsNullOrEmpty(res.OTP))
            {
                _logger.LogInformation(LoggerMessaging.StartPublishing(APPLICATION_SERVICE.AUTH_SERVICE, nameof(SendMailCommand), _handlerName));
                await _publisher.Publish<ISendMailCommand>(new SendMailCommand()
                {
                    To = res.Email,
                    Type = MAIL_TYPE.REGISTATION,
                    Payloads = new Dictionary<string, string>()
                    {
                            { "email", res.Email },
                            { "name", res.Name },
                            { "OTP", res.OTP }
                        }
                });
                _logger.LogInformation(LoggerMessaging.CompletePublishing(APPLICATION_SERVICE.AUTH_SERVICE, nameof(SendMailCommand), _handlerName));
            }

            return res != null;
        }
    }
}
