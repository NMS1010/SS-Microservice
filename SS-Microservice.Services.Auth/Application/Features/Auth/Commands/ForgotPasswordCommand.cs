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
    public class ForgotPasswordCommand : ForgotPasswordRequest, IRequest<bool>
    {
    }

    public class ForgotPasswordHandler : IRequestHandler<ForgotPasswordCommand, bool>
    {
        private readonly IAuthService _authService;
        private readonly ISendEndpointProvider _sendEndpoint;
        private readonly ILogger<ForgotPasswordHandler> _logger;
        private const string _handlerName = nameof(ForgotPasswordHandler);

        public ForgotPasswordHandler(IAuthService authService, ILogger<ForgotPasswordHandler> logger, ISendEndpointProvider sendEndpoint)
        {
            _authService = authService;
            _logger = logger;
            _sendEndpoint = sendEndpoint;
        }

        public async Task<bool> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var res = await _authService.ForgotPassword(request);

            if (res != null && !string.IsNullOrEmpty(res.OTP))
            {
                _logger.LogInformation(LoggerMessaging.StartPublishing(APPLICATION_SERVICE.AUTH_SERVICE, nameof(SendMailCommand), _handlerName));
                await (await _sendEndpoint.GetSendEndpoint(new Uri($"queue:{EventBusConstant.SendMail}")))
                       .Send<ISendMailCommand>(new SendMailCommand()
                       {
                           To = res.Email,
                           Type = MAIL_TYPE.FORGOT_PASSWORD,
                           Payloads = new Dictionary<string, string>()
                        {
                            { "name", res.Name },
                            { "email", res.Email },
                            { "OTP", res.OTP }
                        }
                       });
                _logger.LogInformation(LoggerMessaging.CompletePublishing(APPLICATION_SERVICE.AUTH_SERVICE, nameof(SendMailCommand), _handlerName));
            }

            return res != null;
        }
    }
}
