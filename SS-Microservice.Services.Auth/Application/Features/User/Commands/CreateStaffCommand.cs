using AutoMapper;
using MassTransit;
using MediatR;
using SS_Microservice.Common.Logging.Messaging;
using SS_Microservice.Common.RabbitMQ;
using SS_Microservice.Common.Types.Enums;
using SS_Microservice.Contracts.Commands.Address;
using SS_Microservice.Contracts.Events.User;
using SS_Microservice.Services.Auth.Application.Features.Address.Commands;
using SS_Microservice.Services.Auth.Application.Features.Auth.Events;
using SS_Microservice.Services.Auth.Application.Interfaces;
using SS_Microservice.Services.Auth.Application.Model.User;

namespace SS_Microservice.Services.Auth.Application.Features.User.Commands
{
    public class CreateStaffCommand : CreateStaffRequest, IRequest<string>
    {
    }

    public class CreateStaffHandler : IRequestHandler<CreateStaffCommand, string>
    {
        private readonly IUserService _userService;
        private readonly ISendEndpointProvider _sendEndpoint;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateStaffHandler> _logger;
        private const string _handlerName = nameof(CreateStaffHandler);

        public CreateStaffHandler(IUserService userService, ISendEndpointProvider sendEndpoint,
            IMapper mapper, ILogger<CreateStaffHandler> logger, IPublishEndpoint publishEndpoint)
        {
            _userService = userService;
            _sendEndpoint = sendEndpoint;
            _mapper = mapper;
            _logger = logger;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<string> Handle(CreateStaffCommand request, CancellationToken cancellationToken)
        {
            var id = await _userService.CreateStaff(request);

            if (!string.IsNullOrEmpty(id))
            {
                _logger.LogInformation(LoggerMessaging.StartPublishing(APPLICATION_SERVICE.AUTH_SERVICE, nameof(UserRegistedEvent), _handlerName));
                await _publishEndpoint.Publish<IUserRegistedEvent>(new UserRegistedEvent()
                {
                    Email = request.Email,
                    UserId = id,
                });
                _logger.LogInformation(LoggerMessaging.CompletePublishing(APPLICATION_SERVICE.AUTH_SERVICE, nameof(UserRegistedEvent), _handlerName));

                _logger.LogInformation(LoggerMessaging.StartPublishing(APPLICATION_SERVICE.AUTH_SERVICE, nameof(CreateAddressCommand), _handlerName));
                request.Address.UserId = id;

                await (await _sendEndpoint.GetSendEndpoint(new Uri($"queue:{EventBusConstant.CreateAddress}")))
                    .Send<ICreateAddressCommand>(_mapper.Map<CreateAddressCommand>(request.Address), cancellationToken);

                _logger.LogInformation(LoggerMessaging.CompletePublishing(APPLICATION_SERVICE.AUTH_SERVICE, nameof(CreateAddressCommand), _handlerName));
            }

            return id;
        }
    }
}