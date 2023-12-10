using AutoMapper;
using MassTransit;
using MediatR;
using SS_Microservice.Common.Logging.Messaging;
using SS_Microservice.Common.Types.Enums;
using SS_Microservice.Contracts.Commands.Address;
using SS_Microservice.Services.Auth.Application.Features.Address.Commands;
using SS_Microservice.Services.Auth.Application.Interfaces;
using SS_Microservice.Services.Auth.Application.Model.User;

namespace SS_Microservice.Services.Auth.Application.Features.User.Commands
{
    public class UpdateStaffCommand : UpdateStaffRequest, IRequest<bool>
    {
    }

    public class UpdateStaffHandler : IRequestHandler<UpdateStaffCommand, bool>
    {
        private readonly IUserService _userService;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateStaffHandler> _logger;
        private const string _handlerName = nameof(UpdateStaffHandler);

        public UpdateStaffHandler(IUserService userService, IPublishEndpoint publishEndpoint,
            IMapper mapper, ILogger<CreateStaffHandler> logger)
        {
            _userService = userService;
            _publishEndpoint = publishEndpoint;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<bool> Handle(UpdateStaffCommand request, CancellationToken cancellationToken)
        {
            var userId = await _userService.UpdateStaff(request);
            if (!string.IsNullOrEmpty(userId))
            {

                _logger.LogInformation(LoggerMessaging.StartPublishing(APPLICATION_SERVICE.AUTH_SERVICE,
                    nameof(UpdateAddressCommand), _handlerName));

                var command = _mapper.Map<UpdateAddressCommand>(request.Address);
                command.UserId = userId;
                await _publishEndpoint.Publish<IUpdateAddressCommand>(command, cancellationToken);

                _logger.LogInformation(LoggerMessaging.CompletePublishing(APPLICATION_SERVICE.AUTH_SERVICE,
                    nameof(UpdateAddressCommand), _handlerName));
            }
            return !string.IsNullOrEmpty(userId);
        }
    }
}