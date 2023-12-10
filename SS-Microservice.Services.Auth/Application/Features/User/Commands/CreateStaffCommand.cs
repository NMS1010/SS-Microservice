using AutoMapper;
using MassTransit;
using MediatR;
using SS_Microservice.Common.Logging.Messaging;
using SS_Microservice.Common.Messages.Events.User;
using SS_Microservice.Common.Types.Enums;
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
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateStaffHandler> _logger;
        private const string _handlerName = nameof(CreateStaffHandler);

        public CreateStaffHandler(IUserService userService, IPublishEndpoint publishEndpoint,
            IMapper mapper, ILogger<CreateStaffHandler> logger)
        {
            _userService = userService;
            _publishEndpoint = publishEndpoint;
            _mapper = mapper;
            _logger = logger;
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

                await _publishEndpoint.Publish(_mapper.Map<CreateAddressCommand>(request.Address));

                _logger.LogInformation(LoggerMessaging.CompletePublishing(APPLICATION_SERVICE.AUTH_SERVICE, nameof(CreateAddressCommand), _handlerName));
            }

            return id;
        }
    }
}