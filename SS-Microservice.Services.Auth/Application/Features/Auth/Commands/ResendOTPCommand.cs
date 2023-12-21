using MassTransit;
using MediatR;
using SS_Microservice.Common.Logging.Messaging;
using SS_Microservice.Common.RabbitMQ;
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
        private readonly ISendEndpointProvider _sendEndpoint;
        private readonly ILogger<ResendOTPHandler> _logger;
        private const string _handlerName = nameof(ResendOTPHandler);

        public ResendOTPHandler(IAuthService authService, ILogger<ResendOTPHandler> logger, ISendEndpointProvider sendEndpoint)
        {
            _authService = authService;
            _logger = logger;
            _sendEndpoint = sendEndpoint;
        }

        public async Task<bool> Handle(ResendOTPCommand request, CancellationToken cancellationToken)
        {
            var res = await _authService.ResendOTP(request);
            if (res != null && !string.IsNullOrEmpty(res.OTP))
            {
                _logger.LogInformation(LoggerMessaging.StartPublishing(APPLICATION_SERVICE.AUTH_SERVICE, nameof(SendMailCommand), _handlerName));
                await (await _sendEndpoint.GetSendEndpoint(new Uri($"queue:{EventBusConstant.SendMail}")))
                        .Send<ISendMailCommand>(new SendMailCommand()
                        {
                            To = res.Email,
                            Type = MAIL_TYPE.RESEND,
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
